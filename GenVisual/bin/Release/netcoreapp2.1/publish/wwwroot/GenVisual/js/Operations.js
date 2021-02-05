function findShortestRoute(routes) {
    var shortestRoute = routes[0];
    for (var i = 1; i < routes.length; i++) {
        if (routes[i]["length"] < shortestRoute["length"]) {
            shortestRoute = routes[i];
        }
    }

    return shortestRoute;
}

function countDistinctRoutes(routes) {
    var sRoutes = [];
    for (var i = 0; i < routes.length; i++) {
        sRoutes.push(routes[i]["targetsPoint"].toString());
    }

    sDistinctRoutes = Array.from(new Set(sRoutes));
    return sDistinctRoutes.length;

}

function DesvioPadrao(list) {

    var dp = 0;
    var media;
    var soma = 0;
    for (var i = 0; i < list.length; i++) {
        soma += list[i];
    }
    media = soma / list.length;

    for (var i = 0; i < list.length; i++) {
        dp += Math.pow(list[i] - media, 2);
    }

    dp = Math.sqrt(dp / (list.length - 1));
    return dp;
}