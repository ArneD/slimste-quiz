
namespace SlimsteMens.Service.DataContracts
{
    public class ResetGameDto
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
        public TurnTypeDto Turn { get; set; }
    }
}
