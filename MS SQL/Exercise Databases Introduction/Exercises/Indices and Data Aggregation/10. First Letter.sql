SELECT DISTINCT
	LEFT(FirstName, 1) AS FirstLetter
FROM WizzardDeposits as wd
WHERE DepositGroup = 'Troll Chest'