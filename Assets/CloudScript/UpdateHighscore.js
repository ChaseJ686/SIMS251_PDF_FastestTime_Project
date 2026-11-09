handlers.UpdateHighscore = function(args, context) {
    var score = args.score;
    if (!ScoreIsPossible(score)) return null;
    var request = {
        PlayFabId: currentPlayerId,
        Statistics: [{ StatisticName: "FastestTime", Value: score }]
    };
    return server.UpdatePlayerStatistics(request);
};
function ScoreIsPossible(score) {
    if (score > -1000) return false;
    else return true;
}
