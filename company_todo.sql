-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Anamakine: 127.0.0.1
-- Üretim Zamanı: 06 Oca 2021, 07:38:59
-- Sunucu sürümü: 10.4.13-MariaDB
-- PHP Sürümü: 7.4.7

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `company_todo`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `admin`
--

CREATE TABLE `admin` (
  `admin_id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Tablo döküm verisi `admin`
--

INSERT INTO `admin` (`admin_id`, `username`, `password`) VALUES
(1, 'test', '12345'),
(2, 'admin', '12345');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `department`
--

CREATE TABLE `department` (
  `dep_id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL,
  `description` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Tablo döküm verisi `department`
--

INSERT INTO `department` (`dep_id`, `name`, `description`) VALUES
(1, 'updated', 'lorem ipsum upddd'),
(2, 'muhasebe', 'lorem ipsummm'),
(7, 'deneme', 'lorem ipsum'),
(9, 'yazılımm', 'lorem ipsum upddd');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `employee`
--

CREATE TABLE `employee` (
  `emp_id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `name` varchar(50) NOT NULL,
  `surname` varchar(50) NOT NULL,
  `email` varchar(250) NOT NULL,
  `birthdate` date NOT NULL,
  `dep_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Tablo döküm verisi `employee`
--

INSERT INTO `employee` (`emp_id`, `username`, `password`, `name`, `surname`, `email`, `birthdate`, `dep_id`) VALUES
(1, 'user', '12345', 'yasin', 'tohan', 'deneme@mail.com', '2020-12-01', 1),
(17, 'user2', '12345', 'sasadas', 'sad', 'sadsad', '2020-12-10', 2),
(18, 'user', '12345', 'yasinnn', 'tohannn', 'deneme@mail.com', '2020-01-15', 1),
(19, 'adsads', 'asd', 'adsd', 'dasddasads', 'asd', '2020-12-15', 1),
(20, 'user', '12345', 'yasinnn', 'tohannn', 'deneme@mail.com', '2019-12-19', 1);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `help_msg`
--

CREATE TABLE `help_msg` (
  `help_id` int(11) NOT NULL,
  `emp_id` int(11) NOT NULL,
  `title` varchar(250) NOT NULL,
  `text` varchar(250) NOT NULL,
  `task_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Tablo döküm verisi `help_msg`
--

INSERT INTO `help_msg` (`help_id`, `emp_id`, `title`, `text`, `task_id`) VALUES
(4, 17, 'Help for Task', 'Task Message example', 2);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `take_task`
--

CREATE TABLE `take_task` (
  `id` int(11) NOT NULL,
  `emp_id` int(11) NOT NULL,
  `task_id` int(11) NOT NULL,
  `taken_date` date NOT NULL,
  `delivered_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Tablo döküm verisi `take_task`
--

INSERT INTO `take_task` (`id`, `emp_id`, `task_id`, `taken_date`, `delivered_date`) VALUES
(1, 17, 3, '2020-12-30', NULL),
(2, 17, 2, '2020-12-31', '2020-12-31');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `task`
--

CREATE TABLE `task` (
  `id` int(11) NOT NULL,
  `title` varchar(250) NOT NULL,
  `text` varchar(250) NOT NULL,
  `creation_date` date NOT NULL,
  `duedate` date NOT NULL,
  `status` tinyint(1) NOT NULL,
  `admin_id` int(11) NOT NULL,
  `dep_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Tablo döküm verisi `task`
--

INSERT INTO `task` (`id`, `title`, `text`, `creation_date`, `duedate`, `status`, `admin_id`, `dep_id`) VALUES
(2, 'titleeek', 'taskkkk', '2020-12-30', '2020-12-19', 1, 1, 2),
(3, 'deneme task', 'deneeme içerik', '2020-12-30', '2021-01-02', 1, 1, 2);

--
-- Dökümü yapılmış tablolar için indeksler
--

--
-- Tablo için indeksler `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`admin_id`);

--
-- Tablo için indeksler `department`
--
ALTER TABLE `department`
  ADD PRIMARY KEY (`dep_id`);

--
-- Tablo için indeksler `employee`
--
ALTER TABLE `employee`
  ADD PRIMARY KEY (`emp_id`),
  ADD KEY `emp_dep_id` (`dep_id`);

--
-- Tablo için indeksler `help_msg`
--
ALTER TABLE `help_msg`
  ADD PRIMARY KEY (`help_id`),
  ADD KEY `help_emp_id` (`emp_id`),
  ADD KEY `help_task_id` (`task_id`);

--
-- Tablo için indeksler `take_task`
--
ALTER TABLE `take_task`
  ADD PRIMARY KEY (`id`),
  ADD KEY `task_task_id` (`task_id`),
  ADD KEY `task_emp_id` (`emp_id`);

--
-- Tablo için indeksler `task`
--
ALTER TABLE `task`
  ADD PRIMARY KEY (`id`),
  ADD KEY `task_admin_id` (`admin_id`),
  ADD KEY `task_dep_id` (`dep_id`);

--
-- Dökümü yapılmış tablolar için AUTO_INCREMENT değeri
--

--
-- Tablo için AUTO_INCREMENT değeri `admin`
--
ALTER TABLE `admin`
  MODIFY `admin_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Tablo için AUTO_INCREMENT değeri `department`
--
ALTER TABLE `department`
  MODIFY `dep_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Tablo için AUTO_INCREMENT değeri `employee`
--
ALTER TABLE `employee`
  MODIFY `emp_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- Tablo için AUTO_INCREMENT değeri `help_msg`
--
ALTER TABLE `help_msg`
  MODIFY `help_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Tablo için AUTO_INCREMENT değeri `take_task`
--
ALTER TABLE `take_task`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Tablo için AUTO_INCREMENT değeri `task`
--
ALTER TABLE `task`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Dökümü yapılmış tablolar için kısıtlamalar
--

--
-- Tablo kısıtlamaları `employee`
--
ALTER TABLE `employee`
  ADD CONSTRAINT `emp_dep_id` FOREIGN KEY (`dep_id`) REFERENCES `department` (`dep_id`);

--
-- Tablo kısıtlamaları `help_msg`
--
ALTER TABLE `help_msg`
  ADD CONSTRAINT `help_emp_id` FOREIGN KEY (`emp_id`) REFERENCES `employee` (`emp_id`),
  ADD CONSTRAINT `help_task_id` FOREIGN KEY (`task_id`) REFERENCES `task` (`id`);

--
-- Tablo kısıtlamaları `take_task`
--
ALTER TABLE `take_task`
  ADD CONSTRAINT `task_emp_id` FOREIGN KEY (`emp_id`) REFERENCES `employee` (`emp_id`),
  ADD CONSTRAINT `task_task_id` FOREIGN KEY (`task_id`) REFERENCES `task` (`id`);

--
-- Tablo kısıtlamaları `task`
--
ALTER TABLE `task`
  ADD CONSTRAINT `task_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin` (`admin_id`),
  ADD CONSTRAINT `task_dep_id` FOREIGN KEY (`dep_id`) REFERENCES `department` (`dep_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
