
namespace CommonReg.MigrationRunner.Migrations.Queries
{
    public static class InitialQueries
    {

        public const string CREATE_EXTENSIONS = @"
CREATE EXTENSION IF NOT EXISTS ""uuid-ossp"";
";

        public const string CREATE_USERS = @"
CREATE TABLE IF NOT EXISTS users(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    first_name varchar(120) NOT NULL,
    last_name varchar(120) NOT NULL,
    email varchar(120) NOT NULL,
    age int NOT NULL,
    password_salt uuid NOT NULL,
    password_hash varchar(240) NOT NULL,
    created_date timestamptz NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_date timestamptz NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_active bool NOT NULL DEFAULT false,
    activation_code uuid NOT NULL DEFAULT uuid_generate_v4(),
    CONSTRAINT user_pkey PRIMARY KEY(id)
);";

        public const string CREATE_USER_SESSIONS = @"
CREATE TABLE IF NOT EXISTS user_sessions(
    session_id serial NOT NULL,
    user_id uuid NOT NULL,
    refresh_token uuid NOT NULL,
    user_agent varchar(1024) NOT NULL,
    created_date timestamptz NOT NULL,
    updated_date timestamptz NOT NULL,
    expire_date timestamptz NOT NULL,
    expire_refresh_date timestamptz NOT NULL,
    CONSTRAINT user_sessions_pkey PRIMARY KEY(session_id),
	CONSTRAINT user_fkey FOREIGN KEY(user_id) REFERENCES users(id) NOT VALID
);
";

        public const string CREATE_ROLE = @"
CREATE TABLE IF NOT EXISTS role
(
    id integer NOT NULL,
    name varchar(20) NOT NULL,
    system_name varchar(20) NOT NULL,
    description varchar(200) NOT NULL,
    CONSTRAINT role_pkey PRIMARY KEY (id)
);";

        public const string CREATE_USER_ROLES = @"
CREATE TABLE IF NOT EXISTS user_roles
(
    user_id uuid,
    role_id integer,
    CONSTRAINT user_role_to_roles FOREIGN KEY (role_Id)
        REFERENCES role (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    CONSTRAINT user_role_to_users FOREIGN KEY (user_Id)
        REFERENCES users (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
);";

        public const string CREATE_USER_FORGOT_PASSWORD = @"
CREATE TABLE IF NOT EXISTS user_forgot_password(
    id serial NOT NULL,
    user_id uuid NOT NULL,
    code uuid NOT NULL,
    is_active bool NOT NULL,
    created_date timestamptz NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_date timestamptz NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT user_forgot_password_pkey PRIMARY KEY (id),
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES users(id)
);";

        public const string DROP_TABLES = @"
DROP TABLE IF EXISTS user_sessions;
DROP TABLE IF EXISTS user_roles;
DROP TABLE IF EXISTS user_forgot_password;
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS role;
";

        public const string INSERT_ROLES_DATA = @"
INSERT INTO role
(id, name, system_name, description)
VALUES(1, 'User', 'User', 'Simple user'),
      (2,'Admin','Admin', 'Admin');";

        public const string DROP_EXTENSIONS = @"
DROP EXTENSION IF EXISTS ""uuid-ossp"";
";
    }
}
