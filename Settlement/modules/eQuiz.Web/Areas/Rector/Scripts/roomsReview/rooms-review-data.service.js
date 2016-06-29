(function (angular) {

    angular
        .module("settlementModule")
        .factory("roomsReviewDataService", roomsReviewDataService);

    roomsReviewDataService.$inject = ["$http"];

    function roomsReviewDataService($http) {

        var service = {
            getHostels: getHostelsAjaxMock,
            getRooms: getRoomsAjaxMock
        };

        return service;

        function getHostelsAjax() {
            var promise = $http.get('/Admin/Rooms/GetHostels');
            return promise;
        }

        function getRoomsAjax() {
            var promise = $http.get('/Admin/Rooms/GetRooms');
            return promise;
        }

        function getHostelsAjaxMock() {

            return [
                {
                    id: 1,
                    name: "First"
                },
                {
                    id: 2,
                    name: "Second"
                },
                {
                    id: 3,
                    name: "Third"
                },
                {
                    id: 4,
                    name: "Fourth"
                },
                {
                    id: 5,
                    name: "Fifth"
                },
                {
                    id: 6,
                    name: "Sixth"
                },
                {
                    id: 7,
                    name: "Seventh"
                },
                {
                    id: 8,
                    name: "Eighth"
                },
                {
                    id: 9,
                    name: "Nineth"
                },
                {
                    id: 10,
                    name: "Tenth"
                },
                {
                    id: 11,
                    name: "Eleventh"
                },
                {
                    id: 12,
                    name: "Twelveth"
                }
            ];
        }

        function getRoomsAjaxMock(id) {

            return [
                {

                },
                {

                },
                {

                }
            ]
        }
    }

})(angular);