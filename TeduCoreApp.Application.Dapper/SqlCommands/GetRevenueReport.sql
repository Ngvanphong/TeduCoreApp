alter PROC GetRevenueDaily
	@fromDate VARCHAR(10),
	@toDate VARCHAR(10)
AS
BEGIN		  
		 select
                CAST(b.DateCreated AS DATE) as Date,
				sum(b.TotalMoneyPayment) as Revenue,
				sum(b.TotalMoneyPayment-b.TotalOriginalPrice-b.FeeShipping) as Profit
				from dbo.Bills b
				where b.DateCreated <= cast(@toDate as date) 
				AND b.DateCreated >= cast(@fromDate as date) 
				 group by CAST(b.DateCreated AS DATE);
END

EXEC dbo.GetRevenueDaily @fromDate = '01/01/2017',
                         @toDate = '03/31/2019' 
