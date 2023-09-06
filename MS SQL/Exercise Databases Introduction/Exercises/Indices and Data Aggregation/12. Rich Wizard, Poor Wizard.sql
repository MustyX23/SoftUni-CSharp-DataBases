WITH WizardDeposits AS (
    SELECT
        Id,
        DepositAmount,
        LEAD(DepositAmount) OVER (ORDER BY Id) AS NextWizardDeposit
    FROM WizzardDeposits
)

SELECT
    ABS(SUM(COALESCE(NextWizardDeposit - DepositAmount, 0))) AS TotalDepositDifference
FROM WizardDeposits
WHERE NextWizardDeposit IS NOT NULL;


