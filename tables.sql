CREATE TABLE [dbo].[reserves] (
    [reserve_id]      INT           IDENTITY (1, 1) NOT NULL,
    [user_id]         INT           NOT NULL,
    [product_id]      INT           NOT NULL,
    [reserve_qty]     INT           NOT NULL,
    [reserve_succeed] BIT           DEFAULT ((0)) NOT NULL,
    [reserve_date]    DATETIME2 (7) NULL,
    PRIMARY KEY CLUSTERED ([reserve_id] ASC),
    CONSTRAINT [FK_reserves_product_info] FOREIGN KEY ([product_id]) REFERENCES [dbo].[product_info] ([product_id]) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[product_info] (
    [product_id]          INT           NOT NULL,
    [product_name]        VARCHAR (100) NOT NULL,
    [product_description] TEXT          NULL,
    [qty]                 INT           NULL,
    PRIMARY KEY CLUSTERED ([product_id] ASC),
    CHECK ([qty]>=(0))
);

