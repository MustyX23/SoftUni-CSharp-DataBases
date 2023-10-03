CREATE FUNCTION ufn_IsWordComprised  (@setOfLetters nvarchar(255), @word nvarchar(255))
RETURNS bit
AS
BEGIN 
    DECLARE @l int = 1;
    DECLARE @exist bit = 1;
    WHILE LEN(@word) >= @l AND @exist > 0
    BEGIN
      DECLARE @charindex int; 
      DECLARE @letter char(1);
      SET @letter = SUBSTRING(@word, @l, 1)
      SET @charindex = CHARINDEX(@letter, @setOfLetters, 0)
      SET @exist =
        CASE
            WHEN  @charindex > 0 THEN 1
            ELSE 0
        END;
      SET @l += 1;
    END

    RETURN @exist
END