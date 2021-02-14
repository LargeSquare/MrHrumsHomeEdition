--Orders


create table [TypeOfDelivery]
(
	Id int primary key identity not null,
	Type nvarchar(50) not null,
	Visible bit default 1 not null,
	IsSystem bit default 0 not null
);
go

create table [TypeOfSale]
(
	Id int primary key identity not null,
	Type nvarchar(50) not null,
	Visible bit default 1 not null,
	IsSystem bit default 0 not null
);
go

create table [Client]
(
	Id int primary key identity not null,
	Name nvarchar(50) not null,
	Address nvarchar(255) not null,
	[Number] nvarchar(50) not null,
	Note nvarchar(255) null,
	[Visible] bit default 1 not null
);
go



create table [Order]
(
	Id int primary key identity not null,
	ClientID int not null,
	TypeOfDeliveryID int not null,
	CostOfDelivery decimal not null default 0,
	Amount decimal not null,
	CarriedOut bit default 0 not null,
	Paid bit default 0 not null,
	DeliveryPaid bit default 0 not null,
	Note nvarchar(255) null,
	[Date] datetime default (getdate())
);
go

create table [PositionInOrder]
(
	Id int primary key identity not null,
	OrderID int not null,
	FoodID int not null,
	TypeOfSaleID int not null,
	CountOfBags int not null default 0,
	CountOfKG int not null default 0,
	IndividualPrice decimal not null default 0,
	Discount decimal not null default 0,
	Amount decimal not null,
	CarriedOut bit default 0 not null,
	Paid bit default 0 not null
);
go


alter table [Order] add constraint [FK_Order_ClientID]
	foreign key ([ClientID]) references [Client]([Id]);
	go
	
alter table [Order] add constraint [FK_Order_TypeOfDeliveryID]
	foreign key ([TypeOfDeliveryID]) references [TypeOfDelivery]([Id]);
	go
	

alter table [PositionInOrder] add constraint [FK_PositionInOrder_OrderID]
	foreign key ([OrderID]) references [Order]([Id]);
	go
	
alter table [PositionInOrder] add constraint [FK_PositionInOrder_FoodID]
	foreign key ([FoodID]) references [Food]([Id]);
	go

alter table [PositionInOrder] add constraint [FK_PositionInOrder_TypeOfSaleID]
	foreign key ([TypeOfSaleID]) references [TypeOfSale]([Id]);
	go
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	