CREATE DATABASE IF NOT EXISTS english_now_demo;

USE english_now_demo;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`aluno` (
  `aluno_id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(200) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`aluno_id`))
ENGINE = InnoDB