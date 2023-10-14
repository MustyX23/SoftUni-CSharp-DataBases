CREATE FUNCTION udf_GetTouristsCountOnATouristSite (@Site VARCHAR(100))
RETURNS INT
AS
BEGIN
    DECLARE @TouristCount INT;
    SELECT @TouristCount = COUNT(*) 
    FROM Sites s
    JOIN SitesTourists st ON s.Id = st.SiteId
    WHERE s.Name = @Site;    
    RETURN @TouristCount;
END;