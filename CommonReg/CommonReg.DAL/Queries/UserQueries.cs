namespace CommonProject.DAL.Queries;
public static class UserQueries
{
    public const string GET_USER_BY_EMAIL = @"SELECT * FROM users WHERE LOWER(email) = @Email;";

    public const string GET_USER_BY_ID = @"SELECT * FROM users WHERE id = @Id;";

    public const string GET_ALL_USERS = @"SELECT * FROM users";

    public const string INSERT_USER = @"
INSERT INTO users (
     id
    ,username
    ,first_name
    ,last_name
    ,email
    ,age
    ,password_salt
    ,password_hash
    ,created_at
    ,updated_at
    ,is_active
    ,activation_code
    ,avatar
    )
VALUES (
    @Id
    ,@Username
    ,@FirstName
    ,@LastName
    ,@Email
    ,@Age
    ,@PasswordSalt
    ,@PasswordHash
    ,@CreatedAt
    ,@UpdatedAt
    ,@IsActive
    ,@ActivationCode
    ,@Avatar
    );";

    public const string UPDATE_PASSWORD = @"UPDATE users
                        SET password_salt = @PasswordSalt, password_hash = @PasswordHash, updated_at = @UpdatedAt
                        WHERE id = @Id;";

    public const string UPDATE_USER = @"UPDATE users
                        SET first_name = @FirstName, last_name = @LastName, age = @Age, updated_at = @UpdatedAt, avatar = @Avatar
                        WHERE id = @Id;";

    public const string INSERT_USER_FORGOT_PASSWORD = @"INSERT INTO user_forgot_password (user_id, code, is_active, created_at) 
                      VALUES (@UserId, @Code, @IsActive, @CreatedAt);";

    public const string UPDATE_ACTIVE_STATUS = @"
UPDATE users
SET is_active = @IsActive
WHERE id = @Id";

    public const string SELECT_FORGOT_PASSWORD_BY_ID_AND_CODE = @"SELECT * FROM user_forgot_password WHERE user_id = @Id AND code = @Code AND is_active = true ;";

    public const string SET_INACTIVE_FORGOT_PASSWORD =
      @"UPDATE user_forgot_password
      SET is_active = false
      WHERE user_id = @UserId;";

    public const string DELETE_USER = @"
DELETE FROM user_forgot_password
WHERE user_id = @UserId;

DELETE FROM user_sessions
WHERE user_id = @UserId;

DELETE FROM users
WHERE id = @UserId 
;";
}
