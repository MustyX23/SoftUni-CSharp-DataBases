SELECT wd.DepositGroup, MAX(wd.MagicWandSize) AS [LongestMagicWand]
FROM WizzardDeposits as wd
GROUP BY (wd.DepositGroup)