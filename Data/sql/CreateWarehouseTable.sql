-- warehouse
create table [Warehouse]
(
	Id int primary key identity not null,
	FoodID int not null,
	BagsID int not null,
	KgID int not null
);
go

create table [Bags]
(
	Id int primary key identity not null,
	[Count] int not null default 0
);
go

create table [KG]
(
	Id int primary key identity not null,
	[Count] int not null default 0
);
go

alter table [Warehouse] add constraint [FK_Warehouse_FoodID]
	foreign key (FoodID) references [Food]([Id]);
go

alter table [Warehouse] add constraint [FK_WarehouseKG_BagsID]
	foreign key (BagsID) references [Bags]([Id]);
go

alter table [Warehouse] add constraint [FK_WarehouseKG_KgID]
	foreign key (KgID) references [KG]([Id]);
go