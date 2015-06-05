
using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    /// <summary>
    /// Reset Game Info
    /// </summary>
    public class ResetGameInfo
    {
        /// <summary>
        /// Gets or sets the team1 seconds.
        /// </summary>
        /// <value>
        /// The team1 seconds.
        /// </value>
        public int Team1Seconds { get; set; }

        /// <summary>
        /// Gets or sets the team2 seconds.
        /// </summary>
        /// <value>
        /// The team2 seconds.
        /// </value>
        public int Team2Seconds { get; set; }

        /// <summary>
        /// Gets or sets the team3 seconds.
        /// </summary>
        /// <value>
        /// The team3 seconds.
        /// </value>
        public int Team3Seconds { get; set; }

        /// <summary>
        /// Gets or sets the turn.
        /// </summary>
        /// <value>
        /// The turn.
        /// </value>
        public TurnTypeInfo Turn { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetGameInfo" /> class.
        /// </summary>
        public ResetGameInfo()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetGameInfo" /> class.
        /// </summary>
        /// <param name="resetGameDto">The reset game dto.</param>
        public ResetGameInfo(ResetGameDto resetGameDto)
        {
            Team1Seconds = resetGameDto.Team1Seconds;
            Team2Seconds = resetGameDto.Team2Seconds;
            Team3Seconds = resetGameDto.Team3Seconds;
            Turn = resetGameDto.Turn.ToInfo();
        }

        /// <summary>
        /// Creates the dto.
        /// </summary>
        /// <returns></returns>
        public ResetGameDto CreateDto()
        {
            return new ResetGameDto
                       {
                           Team1Seconds = Team1Seconds,
                           Team2Seconds = Team2Seconds,
                           Team3Seconds = Team3Seconds,
                           Turn = Turn.ToDto(),
                       };
        }
    }
}
