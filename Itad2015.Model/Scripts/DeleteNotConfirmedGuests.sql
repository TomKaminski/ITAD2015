DELETE FROM Guest
WHERE ConfirmationTime IS NULL AND DATEDIFF(DD,RegistrationTime,GETDATE()) >=2
