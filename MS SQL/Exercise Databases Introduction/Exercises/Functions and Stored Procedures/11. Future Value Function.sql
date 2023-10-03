CREATE FUNCTION ufn_CalculateFutureValue(@Sum DECIMAL(15,4),
@YearlyInterestRate FLOAT,
@NumberOfYears INT )
RETURNS DECIMAL(15,4)
BEGIN
    DECLARE @FutureValue DECIMAL(15,4);

    SET @FutureValue = @Sum * POWER((1 + @YearlyInterestRate), @NumberOfYears)
    
    RETURN @FutureValue
END