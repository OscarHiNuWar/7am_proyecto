using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Apitron.PDF.Kit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using Windows.UI.Xaml.Shapes;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.Content;
using Apitron.PDF.Kit.FixedLayout.Resources.XObjects;
using Apitron.PDF.Kit.Interactive.Annotations;
using Apitron.PDF.Kit.Interactive.Forms;
using Apitron.PDF.Kit.Interactive.Forms.Signature;
using Apitron.PDF.Kit.Interactive.Forms.SignatureSettings;
using Apitron.PDF.Kit.Styles;
using Page = Windows.UI.Xaml.Controls.Page;
using Path = Windows.UI.Xaml.Shapes.Path;

namespace CaptureSignatureAsInkDrawing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Fields

        private InkManager inkManager;
        private Point previousContactPt;
        private uint penID;
        private int touchID;
        private double STROKETHICKNESS;

        #endregion

        #region Сtor and ink manager initialization

        public MainPage()
        {
            this.InitializeComponent();

            // init ink manager            
            inkManager = new InkManager();

            InkCanvas.PointerPressed += new PointerEventHandler(InkCanvas_PointerPressed);
            InkCanvas.PointerMoved += new PointerEventHandler(InkCanvas_PointerMoved);
            InkCanvas.PointerReleased += new PointerEventHandler(InkCanvas_PointerReleased);
            InkCanvas.PointerExited += new PointerEventHandler(InkCanvas_PointerReleased);

            STROKETHICKNESS = 5;
        }

        #endregion

        #region Ink drawing code, based on MS sample from https://msdn.microsoft.com/en-us/library/windows/apps/xaml/dn792131.aspx

        private double Distance(Point currentContact, Point previousContact)
        {
            return Math.Sqrt(Math.Pow(currentContact.X - previousContact.X, 2) +
                             Math.Pow(currentContact.Y - previousContact.Y, 2));
        }

        public void InkCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // Get information about the pointer location.
            PointerPoint pt = e.GetCurrentPoint(InkCanvas);
            previousContactPt = pt.Position;

            // Accept input only from a pen or mouse with the left button pressed. 
            PointerDeviceType pointerDevType = e.Pointer.PointerDeviceType;
            if (pointerDevType == PointerDeviceType.Pen ||
                pointerDevType == PointerDeviceType.Mouse &&
                pt.Properties.IsLeftButtonPressed)
            {
                // Pass the pointer information to the InkManager.
                inkManager.ProcessPointerDown(pt);
                penID = pt.PointerId;

                e.Handled = true;
            }

            else if (pointerDevType == PointerDeviceType.Touch)
            {
                // Process touch input
            }
        }

        public void InkCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerId == penID)
            {
                PointerPoint pt = e.GetCurrentPoint(InkCanvas);

                // Render a red line on the canvas as the pointer moves. 
                // Distance() is an application-defined function that tests
                // whether the pointer has moved far enough to justify 
                // drawing a new line.
                Point currentContactPt = pt.Position;
                if (Distance(currentContactPt, previousContactPt) > 2)
                {
                    Line line = new Line()
                                {
                                    X1 = previousContactPt.X,
                                    Y1 = previousContactPt.Y,
                                    X2 = currentContactPt.X,
                                    Y2 = currentContactPt.Y,
                                    StrokeThickness = STROKETHICKNESS,
                                    Stroke = new SolidColorBrush(Colors.Red)
                                };

                    previousContactPt = currentContactPt;

                    // Draw the line on the canvas by adding the Line object as
                    // a child of the Canvas object.
                    InkCanvas.Children.Add(line);

                    // Pass the pointer information to the InkManager.
                    inkManager.ProcessPointerUpdate(pt);
                }
            }

            else if (e.Pointer.PointerId == touchID)
            {
                // Process touch input
            }

            e.Handled = true;
        }

        public void InkCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerId == penID)
            {
                PointerPoint pt = e.GetCurrentPoint(InkCanvas);

                // Pass the pointer information to the InkManager. 
                inkManager.ProcessPointerUp(pt);
            }

            else if (e.Pointer.PointerId == touchID)
            {
                // Process touch input
            }

            touchID = 0;
            penID = 0;

            // Call an application-defined function to render the ink strokes.
            RenderAllStrokes();

            e.Handled = true;
        }

        private void RenderAllStrokes()
        {
            // Clear the canvas.
            InkCanvas.Children.Clear();

            // Get the InkStroke objects.
            IReadOnlyList<InkStroke> inkStrokes = inkManager.GetStrokes();

            // Process each stroke.
            foreach (InkStroke inkStroke in inkStrokes)
            {
                PathGeometry pathGeometry = new PathGeometry();
                PathFigureCollection pathFigures = new PathFigureCollection();
                PathFigure pathFigure = new PathFigure();
                PathSegmentCollection pathSegments = new PathSegmentCollection();

                // Create a path and define its attributes.
                Path path = new Path();
                path.Stroke = new SolidColorBrush(Colors.Red);
                path.StrokeThickness = STROKETHICKNESS;

                // Get the stroke segments.
                IReadOnlyList<InkStrokeRenderingSegment> segments;
                segments = inkStroke.GetRenderingSegments();

                // Process each stroke segment.
                bool first = true;
                foreach (InkStrokeRenderingSegment segment in segments)
                {
                    // The first segment is the starting point for the path.
                    if (first)
                    {
                        pathFigure.StartPoint = segment.BezierControlPoint1;
                        first = false;
                    }

                    // Copy each ink segment into a bezier segment.
                    BezierSegment bezSegment = new BezierSegment();
                    bezSegment.Point1 = segment.BezierControlPoint1;
                    bezSegment.Point2 = segment.BezierControlPoint2;
                    bezSegment.Point3 = segment.Position;

                    // Add the bezier segment to the path.
                    pathSegments.Add(bezSegment);
                }

                // Build the path geometerty object.
                pathFigure.Segments = pathSegments;
                pathFigures.Add(pathFigure);
                pathGeometry.Figures = pathFigures;

                // Assign the path geometry object as the path data.
                path.Data = pathGeometry;

                // Render the path by adding it as a child of the Canvas object.
                InkCanvas.Children.Add(path);
            }
        }

        /// <summary>
        /// Clears the signature manager and corresponding signature panel.
        /// </summary>
        private void ClearSignatureAreaClick(object sender, RoutedEventArgs e)
        {
            InkCanvas.Children.Clear();

            foreach (InkStroke inkStroke in inkManager.GetStrokes())
            {
                inkStroke.Selected = true;
            }

            inkManager.DeleteSelected();
        }

        #endregion

        #region PDF generation code

        /// <summary>
        /// Handles Save to PDF button click event and performs saving to PDF
        /// </summary>
        private async void SaveToPDFClick(object sender, RoutedEventArgs e)
        {
            // create PDF document and append new page
            FixedDocument document = new FixedDocument();

            Apitron.PDF.Kit.FixedLayout.Page pdfPage = new Apitron.PDF.Kit.FixedLayout.Page(Boundaries.A4);
            document.Pages.Add(pdfPage);

            // create signature drawing based on strokes captured by ink manager
            var signatureContent = CreateSignatureDrawing();

            // register signature drawing as XObject for future reference
            document.ResourceManager.RegisterResource(new FixedContent("signatureDrawing",new Boundary(0,0,inkPanel.Width,inkPanel.Height),signatureContent));
          
            // create signature field, set its settings and append to document
            SignatureField signatureField = await CreateSignatureField();
            signatureField.ViewSettings = new SignatureFieldViewSettings(){Description =  Description.None, Graphic = Graphic.XObject, GraphicResourceID = "signatureDrawing"};
            document.AcroForm.Fields.Add(signatureField);
            
            // scale factor for signature
            double signatureScaleFactor = 0.3;

            // add signature view widget on PDF page, it's being linked to corresponding field and its size is being set using scale factor defined above
            pdfPage.Annotations.Add(new SignatureFieldView(signatureField, new Boundary(0,Boundaries.A4.Top-inkPanel.Height*signatureScaleFactor,inkPanel.Width*signatureScaleFactor,Boundaries.A4.Top)));

            // create output file
            StorageFile outputFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("signature.pdf", CreationCollisionOption.ReplaceExisting);

            // save resulting PDF to local storage, can be found at
            // C:\Users\[user name]\AppData\Local\Packages\9ecb51ab-7479-453d-afac-ed9ee63584da_729yje1rdqdg0\LocalState\signature.pdf
            using (Stream stream = await outputFile.OpenStreamForWriteAsync())
            {
                document.Save(stream);
            }
        }

        /// <summary>
        /// Create signature drawing consisting of PDF drawing commands translated from captured by ink manager.
        /// </summary>
        private ClippedContent CreateSignatureDrawing()
        {
            ClippedContent signatureContent = new ClippedContent(0, 0, inkPanel.Width, inkPanel.Height);

            // set stroking parameters
            signatureContent.SetDeviceStrokingColor(RgbColors.Red.Components);
            signatureContent.SetLineWidth(STROKETHICKNESS);

            // since PDF coordinate system has inverted Y axis, we invert it here by applying transformation
            signatureContent.ModifyCurrentTransformationMatrix(1, 0, 0, -1, 0, inkPanel.Height);

            // the ink path
            Apitron.PDF.Kit.FixedLayout.Content.Path strokePath = new Apitron.PDF.Kit.FixedLayout.Content.Path();

            // Get the InkStroke objects
            IReadOnlyList<InkStroke> inkStrokes = inkManager.GetStrokes();

            // Process each stroke
            foreach (InkStroke inkStroke in inkStrokes)
            {
                // Get the stroke segments.
                IReadOnlyList<InkStrokeRenderingSegment> segments;
                segments = inkStroke.GetRenderingSegments();

                // Process each stroke segment.
                bool first = true;
                foreach (InkStrokeRenderingSegment segment in segments)
                {
                    // The first segment is the starting point for the path.
                    if (first)
                    {
                        strokePath.MoveTo(segment.BezierControlPoint1.X, segment.BezierControlPoint1.Y);
                        first = false;
                    }

                    // Copy each ink segment into a bezier segment.
                    BezierSegment bezSegment = new BezierSegment();
                    bezSegment.Point1 = segment.BezierControlPoint1;
                    bezSegment.Point2 = segment.BezierControlPoint2;
                    bezSegment.Point3 = segment.Position;

                    // Add the bezier segment to the path.
                    strokePath.AppendCubicBezier(segment.BezierControlPoint1.X, segment.BezierControlPoint1.Y,
                        segment.BezierControlPoint2.X, segment.BezierControlPoint2.Y, segment.Position.X,
                        segment.Position.Y);
                }
            }

            // stroke the path
            signatureContent.StrokePath(strokePath);
            return signatureContent;
        }

        /// <summary>
        /// Creates signature field by loading the corresponding certificate from app resources.
        /// </summary>
        private async Task<SignatureField> CreateSignatureField()
        {
            SignatureField signatureField = new SignatureField("signature");

            using (Stream signatureDataStream = await Package.Current.InstalledLocation.OpenStreamForReadAsync("data\\JohnDoe.pfx"))
            {
                signatureField.Signature = Signature.Create(new Pkcs12Store(signatureDataStream, "password"));                
            }

            return signatureField;
        }

        #endregion

    }
}
