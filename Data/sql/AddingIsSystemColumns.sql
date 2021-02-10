alter table [BudgetState] add
	[IsSystem] bit default 0 not null;
go

alter table [TypeOfBudgetAction] add
	[IsSystem] bit default 0 not null;
go

alter table [TypeOfDelivery] add
	[IsSystem] bit default 0 not null;
go

alter table [TypeOfSale] add
	[IsSystem] bit default 0 not null;
go

alter table [Event] drop column [IsSystem];
go