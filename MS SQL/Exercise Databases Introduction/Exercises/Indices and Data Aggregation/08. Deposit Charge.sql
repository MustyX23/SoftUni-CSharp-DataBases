SELECT [wd].[DepositGroup], [wd].[MagicWandCreator], MIN([wd].[DepositCharge]) AS [MinDepositCharge]
FROM [WizzardDeposits] as [wd]
GROUP BY wd.DepositGroup, wd.MagicWandCreator
ORDER BY wd.MagicWandCreator, wd.DepositGroup 