-- warehouse
create table [Warehouse]
(
	Id int primary key identity not null,
	FoodID int not null,
	CountOfBags int not null default 0,
	CountOfKG int not null default 0
);
go

alter table [Warehouse] add constraint [FK_Warehouse_FoodID]
	foreign key (FoodID) references [Food]([Id]);
go