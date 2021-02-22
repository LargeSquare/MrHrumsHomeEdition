--Event

create table [TypeOfEvent]
(
	Id int primary key identity not null,
	Type nvarchar(50) not null,
	[IsSystem] bit default 0 not null,
	Visible bit default 1 not null
);
go

create table [Event]
(
	Id int primary key identity not null,
	TypeOfEventID int not null,
	[Date] datetime not null default (getdate()),
	Message nvarchar null
);
go

alter table [Event] add constraint [FK_Event_TypeOfBudgetActionID]
	foreign key ([TypeOfEventID]) references [TypeOfEvent]([Id]);
	go