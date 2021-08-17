use bankUsers
go

select * from tbUsers

Insert into tbUsers (name, mainSurname, secondarySurname, degreeBeforeName, degreeAfterName, homeAddress, organizationalUnit,
telNumberWork, telNumberPrivate, emailWork, emailPrivate, employmentFrom, employmentTo, maternityOrParentalLeave)
VALUES ('Jirka', 'Okurka', '', 'Mgr.', '', 'U parku 9874 Aš', 'IT', '776851276', '496518712', 'JirkaOkurka@expo.cz', 'ItsJirkO@seznam.cz',
'1.7.1999', '5.7.2022', '0')

select * from tbUsers