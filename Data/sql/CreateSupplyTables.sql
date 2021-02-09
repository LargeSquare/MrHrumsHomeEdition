-- Supply

create table [Supply]
(
	Id int primary key identity not null,
	Amount money not null,
	Paid bit default 0 not null,
	[Date] datetime default (getdate())
);
go

create table [PositionInSupply]
(
	Id int primary key identity not null,
	SupplyID int not null,
	FoodID int not null,
	CountOfBags int not null,
	Paid bit default 0 not null
);
go

alter table [PositionInSupply] add constraint [FK_PositionInSupply_SupplyID]
	foreign key ([SupplyID]) references [Supply]([Id]);
	go
	
alter table [PositionInSupply] add constraint [FK_PositionInSupply_FoodID]
	foreign key ([FoodID]) references [Food]([Id]);
	go