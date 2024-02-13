DECLARE @IntNumberSum BIGINT;
SELECT @IntNumberSum = SUM(CAST(IntNumber AS BIGINT)) FROM ImportTable;

DECLARE @Median FLOAT;
SELECT @Median = PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY DecimalNumber) OVER() FROM ImportTable;

SELECT @IntNumberSum, @Median;