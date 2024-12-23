CREATE SCHEMA IF NOT EXISTS `english_now_demo` DEFAULT CHARACTER SET utf8;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`papel` (
  `papel_id` INT NOT NULL AUTO_INCREMENT,
  `descricao` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`papel_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`usuario` (
  `usuario_id` INT NOT NULL AUTO_INCREMENT,
  `login` VARCHAR(100) NOT NULL,
  `senha` VARCHAR(100) NOT NULL,
  `papel_id` INT NOT NULL,
  PRIMARY KEY (`usuario_id`),
  UNIQUE INDEX `login_UNIQUE` (`login` ASC) VISIBLE,
  CONSTRAINT `usuario_papel`
    FOREIGN KEY (`papel_id`)
    REFERENCES `english_now_demo`.`papel` (`papel_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`aluno` (
  `aluno_id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(200) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `usuario_id` INT NOT NULL,
  PRIMARY KEY (`aluno_id`),
  UNIQUE INDEX `usuario_id_UNIQUE` (`usuario_id` ASC) VISIBLE,
  CONSTRAINT `aluno_usuario`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `english_now_demo`.`usuario` (`usuario_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`professor` (
  `professor_id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(200) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `usuario_id` INT NOT NULL,
  PRIMARY KEY (`professor_id`),
  UNIQUE INDEX `usuario_id_UNIQUE` (`usuario_id` ASC) VISIBLE,
  CONSTRAINT `professor_usuario`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `english_now_demo`.`usuario` (`usuario_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`turma` (
  `turma_id` INT NOT NULL AUTO_INCREMENT,
  `professor_id` INT NOT NULL,
  `semestre` INT NOT NULL,
  `ano` INT NOT NULL,
  `nivel` VARCHAR(100) NOT NULL,
  `periodo` VARCHAR(200) NOT NULL,
  PRIMARY KEY (`turma_id`),
  CONSTRAINT `turma_professor`
    FOREIGN KEY (`professor_id`)
    REFERENCES `english_now_demo`.`professor` (`professor_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`aluno_turma_boletim` (
  `boletim_id` INT NOT NULL AUTO_INCREMENT,
  `aluno_id` INT NOT NULL,
  `turma_id` INT NOT NULL,
  `nota_bim1_escrita` DECIMAL(4,2) NULL DEFAULT NULL,
  `nota_bim1_leitura` DECIMAL(4,2) NULL DEFAULT NULL,
  `nota_bim1_conversacao` DECIMAL(4,2) NULL DEFAULT NULL,
  `nota_bim1_final` DECIMAL(4,2) NULL DEFAULT NULL,
  `nota_bim2_escrita` DECIMAL(4,2) NULL DEFAULT NULL,
  `nota_bim2_leitura` DECIMAL(4,2) NULL DEFAULT NULL,
  `nota_bim2_conversacao` DECIMAL(4,2) NULL DEFAULT NULL,
  `nota_bim2_final` DECIMAL(4,2) NULL DEFAULT NULL,
  `nota_final_semestre` DECIMAL(4,2) NULL DEFAULT NULL,
  `faltas_semestre` INT NULL DEFAULT NULL,
  PRIMARY KEY (`boletim_id`),
  CONSTRAINT `boletim_turma`
    FOREIGN KEY (`turma_id`)
    REFERENCES `english_now_demo`.`turma` (`turma_id`),
  CONSTRAINT `boletim_aluno`
    FOREIGN KEY (`aluno_id`)
    REFERENCES `english_now_demo`.`aluno` (`aluno_id`))
ENGINE = InnoDB;

INSERT INTO `english_now_demo`.`papel`
(`papel_id`,
`descricao`)
VALUES
(1,
'Administrador');

INSERT INTO `english_now_demo`.`papel`
(`papel_id`,
`descricao`)
VALUES
(2,
'Professor');

INSERT INTO `english_now_demo`.`papel`
(`papel_id`,
`descricao`)
VALUES
(3,
'Aluno');

INSERT INTO `english_now_demo`.`usuario`
(`usuario_id`,
`login`,
`senha`,
`papel_id`)
VALUES
(1,
'admin',
'123',
1);