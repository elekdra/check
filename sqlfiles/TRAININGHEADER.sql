
USE `DOCUMENT_MANAGEMENT`;
Create table TrainingDetails_header (`Training_index` INT NOT NULL auto_increment,`Company_ID` INT NOT NULL,`Version` varchar(50),`Training_ID` INT NOT NULL,primary key(`Training_index`));

INSERT INTO TrainingDetails_header(`Company_ID`,`Version`,`Training_ID`) Values(103,'3.0',2),(100,'5.0',1),(103,'1.0',2),(101,'1.0',2),(101,'1.0',1),(100,'1.0',3),(102,'1.0',2),(100,'1.0',1),(100,'5.0',2),(101,'3.0',1),(101,'3.0',4),(100,'1.0',1),(101,'2.0',1),(100,'3.0',2),(103,'4.0',1),(100,'5.0',3),(103,'5.0',1),(100,'5.0',3),(100,'1.0',1),(103,'5.0',1),(103,'2.0',2),(100,'1.0',4),(100,'5.0',3),(100,'2.0',2);