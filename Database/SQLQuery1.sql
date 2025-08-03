CREATE TABLE tbl_user (
    user_id    INT IDENTITY(1,1) PRIMARY KEY,
    user_name  VARCHAR(255) NOT NULL,
    password   VARCHAR(255) NOT NULL,
    height     INT,
    weight     INT,
    birthdate  VARCHAR(50),
    phone      VARCHAR(50),
    address    VARCHAR(500),
    gender     VARCHAR(10),
    age        INT
);

CREATE TABLE tbl_goal (
    goal_id         INT IDENTITY(1,1) PRIMARY KEY,
    user_id         INT NOT NULL,
    target_calories FLOAT,
    date            VARCHAR(20) NOT NULL,
    is_achieved     BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (user_id) REFERENCES tbl_user(user_id)
);

CREATE TABLE tbl_activity (
    activity_id     INT IDENTITY(1,1) PRIMARY KEY,
    user_id         INT NOT NULL,
    activity_type   VARCHAR(100),
    date            VARCHAR(20),
    duration        INT,
    calories_burned FLOAT,
    metric1         FLOAT,
    metric2         FLOAT,
    metric3         FLOAT,
    FOREIGN KEY (user_id) REFERENCES tbl_user(user_id)
);