CREATE DATABASE IF NOT EXISTS db;
USE db;

CREATE TABLE IF NOT EXISTS Accounts
(
    Id int PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Name varchar(100) DEFAULT '',
    UserId int DEFAULT -1
);

CREATE TABLE IF NOT EXISTS Users
(
    Id int PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Name varchar(100) DEFAULT '',
    Email varchar(100) DEFAULT '',
    Password varchar(100) DEFAULT ''
);

CREATE TABLE IF NOT EXISTS Records
(
    Id int PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Description varchar(100) DEFAULT '',
    Amount float DEFAULT 0.0,
    Status boolean DEFAULT false,
    Date date DEFAULT CURRENT_DATE,
    TypeId int DEFAULT -1,
    AccountId int DEFAULT -1
);

CREATE TABLE IF NOT EXISTS RecordTypes
(
    Id int PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Name varchar(100) DEFAULT '',
    Icon char(1) DEFAULT '',
    Color varchar(10) DEFAULT ''
);
