
USE `DOCUMENT_MANAGEMENT`;
Create table UserCredentials (`USER_ID` INT NOT NULL auto_increment,`USER_NAME` VARCHAR(40) NOT NULL,`PASSWORD` VARCHAR(40) NOT NULL,primary key(`USER_ID`));
ALTER TABLE UserCredentials AUTO_INCREMENT=1;
INSERT INTO UserCredentials(`USER_NAME`,`PASSWORD`) Values('fathima','admin'),('admin','password'),('xyzw','admin123'),('admin','password'),('user','pass'),('@#WXYZ','123456');