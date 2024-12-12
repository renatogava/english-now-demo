CREATE SCHEMA IF NOT EXISTS `english_now_demo` DEFAULT CHARACTER SET utf8;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`periodo` (
  `periodo_id` INT NOT NULL AUTO_INCREMENT,
  `descricao` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`periodo_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`nivel` (
  `nivel_id` INT NOT NULL AUTO_INCREMENT,
  `descricao` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`nivel_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`papel` (
  `papel_id` INT NOT NULL AUTO_INCREMENT,
  `descricao` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`papel_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`usuario` (
  `usuario_id` INT NOT NULL AUTO_INCREMENT,
  `login` VARCHAR(100) NOT NULL,
  `senha` VARCHAR(100) NOT NULL,
  `papel_id` INT(45) NOT NULL,
  PRIMARY KEY (`usuario_id`),
  CONSTRAINT `usuario_papel`
    FOREIGN KEY (`papel_id`)
    REFERENCES `english_now_demo`.`papel` (`papel_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`aluno` (
  `aluno_id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(200) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `usuario_id` INT NOT NULL,
  PRIMARY KEY (`aluno_id`),
  CONSTRAINT `aluno_usuario`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `english_now_demo`.`usuario` (`usuario_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`professor` (
  `professor_id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(200) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `usuario_id` INT NOT NULL,
  PRIMARY KEY (`professor_id`),
  INDEX `professor_usuario_idx` (`usuario_id` ASC) VISIBLE,
  CONSTRAINT `professor_usuario`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `english_now_demo`.`usuario` (`usuario_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`turma` (
  `turma_id` INT NOT NULL AUTO_INCREMENT,
  `semestre` INT NOT NULL,
  `ano` INT NOT NULL,
  `nivel_id` INT NOT NULL,
  `periodo_id` INT NOT NULL,
  `professor_id` INT NOT NULL,
  PRIMARY KEY (`turma_id`),
  CONSTRAINT `turma_professor`
    FOREIGN KEY (`professor_id`)
    REFERENCES `english_now_demo`.`professor` (`professor_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `turma_periodo`
    FOREIGN KEY (`periodo_id`)
    REFERENCES `english_now_demo`.`periodo` (`periodo_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `turma_nivel`
    FOREIGN KEY (`nivel_id`)
    REFERENCES `english_now_demo`.`nivel` (`nivel_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`aluno_turma` (
  `aluno_turma_id` INT NOT NULL AUTO_INCREMENT,
  `aluno_id` INT NOT NULL,
  `turma_id` INT NOT NULL,
  PRIMARY KEY (`aluno_turma_id`),
  CONSTRAINT `aluno_alunoturma`
    FOREIGN KEY (`aluno_id`)
    REFERENCES `english_now_demo`.`aluno` (`aluno_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `turma_alunoturma`
    FOREIGN KEY (`turma_id`)
    REFERENCES `english_now_demo`.`turma` (`turma_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `english_now_demo`.`boletim` (
  `boletim_id` INT NOT NULL AUTO_INCREMENT,
  `aluno_turma_id` INT NOT NULL,
  `nota_bim1_escrita` DECIMAL(4,2) NULL,
  `nota_bim1_leitura` DECIMAL(4,2) NULL,
  `nota_bim1_conversacao` DECIMAL(4,2) NULL,
  `nota_bim1_final` DECIMAL(4,2) NULL,
  `nota_bim2_escrita` DECIMAL(4,2) NULL,
  `nota_bim2_leitura` DECIMAL(4,2) NULL,
  `nota_bim2_conversacao` DECIMAL(4,2) NULL,
  `nota_bim2_final` DECIMAL(4,2) NULL,
  `nota_final_semestre` DECIMAL(4,2) NULL,
  `faltas_semestre` INT NULL,
  PRIMARY KEY (`boletim_id`),
  CONSTRAINT `boletim_alunoturma`
    FOREIGN KEY (`aluno_turma_id`)
    REFERENCES `english_now_demo`.`aluno_turma` (`aluno_turma_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;