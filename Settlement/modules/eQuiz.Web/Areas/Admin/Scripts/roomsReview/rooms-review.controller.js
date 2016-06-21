(function (angular) {
    angular.module('settlementModule').controller('RoomsReviewController', ReviewController);

    ReviewController.$inject = ['$scope', '$filter', 'RoomsreviewDataService', 'rooms'];

    function ReviewController($scope, $filter, roomsReviewDataService, rooms) {
        var vm = this;
        vm.roomsList = rooms;

        function activate() {
            roomsReviewDataService.getRooms().then(function (result) {
                vm.roomsList = result;
            })
        }
    };

})(angular);