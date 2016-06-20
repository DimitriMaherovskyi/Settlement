(function (angular) {

    angular
        .module("settlementModule")
        .factory("roomsReviewDataService", roomsReviewDataService);

    roomsReviewDataService.$inject = ["$http"];

    function roomsReviewDataService($http) {

        var service = {
            getRooms: getRoomsAjax,
        };

        return service;

        function getRoomsAjax() {
            var promise = $http.get('/Admin/Review/GetRooms');
            return promise;
        }
    }

})(angular);