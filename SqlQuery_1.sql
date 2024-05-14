CREATE PROCEDURE EditPharrProfile
    @identificationCode INT,
    @name VARCHAR(50),
    @pharmacy_name VARCHAR(50),
    @first_working_day VARCHAR(50)
    

AS
BEGIN
    UPDATE Pharmacists
    SET 
        name = @name,
        pharmacy_name = @pharmacy_name,
        first_working_day = @first_working_day
    WHERE identificationCode = @identificationCode;
END;
GO