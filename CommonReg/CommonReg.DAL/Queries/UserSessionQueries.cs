namespace CommonReg.DAL.Queries
{

    public static class UserSessionQueries
    {
        public const string GET_SESSION_BY_ID = @"SELECT * FROM user_sessions WHERE session_id = @sessionId;";

        public const string INSERT_NEW_SESSION = @"
DELETE FROM user_sessions
WHERE 
    session_id = ANY(
    SELECT session_id
    FROM user_sessions
    WHERE user_id = @UserId
    ORDER BY updated_date DESC
    OFFSET 4);

INSERT INTO user_sessions (
     user_id
    ,refresh_token
    ,created_date
    ,updated_date
    ,expire_refresh_date
    ,expire_date
    ,user_agent
    )
VALUES (
     @UserId
    ,@RefreshToken
    ,@CreatedDate
    ,@UpdatedDate
    ,@ExpireRefreshDate
    ,@ExpireDate
    ,@UserAgent
    )
RETURNING session_id;";

        public const string UPDATE_SESSION = @"
UPDATE user_sessions
SET  refresh_token      = @RefreshToken
    ,updated_date       = @UpdatedDate
    ,expire_refresh_date = @ExpireRefreshDate
    ,user_agent         = @UserAgent
WHERE session_id = @SessionId AND user_id = @UserId 
RETURNING 
     session_id
    ,user_id
    ,refresh_token
    ,created_date
    ,updated_date
    ,expire_refresh_date
    ,expire_date
    ,user_agent;
";

        public const string DELETE_SESSION_BY_ID = @"
DELETE FROM user_sessions 
WHERE session_id = @sessionId AND user_id = @UserId ;";

        public const string DELETE_SESSIONS_BY_USER_ID = @"
DELETE FROM user_sessions 
WHERE user_id = @UserId ;";
    }

}
