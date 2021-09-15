CREATE TYPE dbo.StringListTableType AS TABLE
(
    item VARCHAR(200) NOT NULL
);
GO

CREATE TYPE dbo.NVarcharStringListTableType AS TABLE
(
    item NVARCHAR(200) NOT NULL
);
GO

CREATE TYPE dbo.LongListTableType AS TABLE
(
    item BIGINT NOT NULL
);
GO

CREATE TYPE dbo.IntListTableType AS TABLE
(
    item INT NOT NULL
);
GO

CREATE TYPE dbo.DecimalListTableType AS TABLE
(
    item DECIMAL(24, 6) NOT NULL
);
GO

CREATE TYPE dbo.DateTimeListTableType AS TABLE
(
    item DateTime NOT NULL
);
GO

CREATE TYPE dbo.DateListTableType AS TABLE
(
    item Date NOT NULL
);
GO

CREATE TYPE dbo.EnumListTableType AS TABLE
(
    num INT NOT NULL,
    [text] VARCHAR(200) NOT NULL
);
GO

CREATE TYPE dbo.StringArrayListTableType AS TABLE
(
    item0 VARCHAR(200) NOT NULL,
    item1 VARCHAR(200) NOT NULL,
    item2 VARCHAR(200) NULL,
    item3 VARCHAR(200) NULL,
    item4 VARCHAR(200) NULL,
    item5 VARCHAR(200) NULL,
    item6 VARCHAR(200) NULL,
    item7 VARCHAR(200) NULL,
    item8 VARCHAR(200) NULL,
    item9 VARCHAR(200) NULL
);
GO