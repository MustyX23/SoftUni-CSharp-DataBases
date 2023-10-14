CREATE PROCEDURE usp_AnnualRewardLottery
    @TouristName nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Calculate the number of sites visited by the tourist
    DECLARE @SiteCount INT;
    SELECT @SiteCount = COUNT(*)
    FROM SitesTourists as st
    JOIN Tourists as t ON t.Id = st.TouristId
    WHERE t.Name = @TouristName;

    -- Update the reward based on the number of sites visited
    UPDATE Tourists
    SET Reward = CASE 
        WHEN @SiteCount >= 100 THEN 'Gold badge'
        WHEN @SiteCount >= 50 THEN 'Silver badge'
        WHEN @SiteCount >= 25 THEN 'Bronze badge'
        ELSE Reward
    END
    WHERE Name = @TouristName;

    -- Select the name and reward of the tourist
    SELECT Name, Reward 
    FROM Tourists 
    WHERE Name = @TouristName;
END;




