-- phpMyAdmin SQL Dump
-- version 4.0.4
-- http://www.phpmyadmin.net
--
-- Servidor: localhost
-- Tiempo de generación: 24-06-2016 a las 20:54:38
-- Versión del servidor: 5.6.12-log
-- Versión de PHP: 5.4.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `7am_proyecto`
--
CREATE DATABASE IF NOT EXISTS `7am_proyecto` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `7am_proyecto`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cliente`
--

CREATE TABLE IF NOT EXISTS `cliente` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(60) NOT NULL,
  `rnc` varchar(60) NOT NULL,
  `telefono` varchar(44) NOT NULL,
  `correo` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Volcado de datos para la tabla `cliente`
--

INSERT INTO `cliente` (`id`, `nombre`, `rnc`, `telefono`, `correo`) VALUES
(1, '7AM', '111111', '809', '7am@gmail.com'),
(2, 'Tecnic', '222222', '8099', 'tec@gmail.com'),
(3, 'Prueba', '1', '2', '3'),
(4, 'Oscar', '0', '0', '0');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `factura`
--

CREATE TABLE IF NOT EXISTS `factura` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_item` int(11) NOT NULL,
  `tipo_pago` varchar(20) NOT NULL,
  `fech_venc` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Volcado de datos para la tabla `factura`
--

INSERT INTO `factura` (`id`, `id_item`, `tipo_pago`, `fech_venc`) VALUES
(2, 2, 'Efectivo', '24/6/16 '),
(3, 3, 'Efectivo', '25/6/16 ');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `items`
--

CREATE TABLE IF NOT EXISTS `items` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_cliente` int(11) NOT NULL,
  `cantidad` int(11) NOT NULL,
  `descripcion` varchar(30) NOT NULL,
  `precio` varchar(11) NOT NULL,
  `fech_venc` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Volcado de datos para la tabla `items`
--

INSERT INTO `items` (`id`, `id_cliente`, `cantidad`, `descripcion`, `precio`, `fech_venc`) VALUES
(3, 1, 1, 'Realización pagina web', 'RD$1,500.00', '24/6/16 '),
(4, 1, 1, 'Diseño de línea grafica', 'RD$3,000.00', '24/6/16 '),
(5, 1, 1, 'Realización pagina web', 'RD$2.00', '25/6/16 '),
(6, 1, 1, 'Diseño de línea grafica', 'RD$4.00', '25/6/16 ');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `nfc`
--

CREATE TABLE IF NOT EXISTS `nfc` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `codigo` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE IF NOT EXISTS `usuarios` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user` varchar(255) NOT NULL,
  `pass` varchar(255) NOT NULL,
  `class` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`id`, `user`, `pass`, `class`) VALUES
(1, 'Oscar', '123', 'administrador');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
