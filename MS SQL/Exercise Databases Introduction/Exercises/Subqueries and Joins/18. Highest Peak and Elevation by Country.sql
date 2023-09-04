WITH PeakData AS (
    SELECT
        c.CountryName AS [Country],
        COALESCE(MAX(p.Elevation), 0) AS MaxElevation,
        STRING_AGG(p.PeakName, ', ') WITHIN GROUP (ORDER BY p.Elevation ASC) AS MaxElevationPeaks,
        STRING_AGG(m.MountainRange, ', ') WITHIN GROUP (ORDER BY p.Elevation ASC) AS MaxElevationMountains
    FROM Countries c
    LEFT JOIN MountainsCountries mc ON mc.CountryCode = c.CountryCode
    LEFT JOIN Mountains m ON m.Id = mc.MountainId
    LEFT JOIN Peaks p ON p.MountainId = m.Id
    GROUP BY c.CountryName
)

SELECT TOP 5
    pd.[Country],
    CASE
        WHEN pd.MaxElevation > 0 THEN pd.MaxElevationPeaks
        ELSE '(no highest peak)'
    END AS [Highest Peak Name],
    pd.MaxElevation AS [Highest Peak Elevation],
    CASE
        WHEN pd.MaxElevation > 0 THEN pd.MaxElevationMountains
        ELSE '(no mountain)'
    END AS [Mountain]
FROM PeakData pd
ORDER BY pd.[Country] ASC, [Highest Peak Name] ASC;
