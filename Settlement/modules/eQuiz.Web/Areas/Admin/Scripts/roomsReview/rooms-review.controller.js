(function (angular) {
    angular.module('settlementModule').controller('RoomsReviewController', ReviewController);

    ReviewController.$inject = ['$scope', '$filter', 'roomsReviewDataService', 'hostels'];

    function ReviewController($scope, $filter, roomsReviewDataService, hostels) {
        var vm = this;
        vm.hostelsList = hostels;
        vm.hostelRooms = [];

        function activate() {
            roomsReviewDataService.getHostels().then(function (result) {
                vm.hostelsList = result;
            })
        }

        vm.showHostelRooms = function (id) {
            roomsReviewDataService.getRooms(id).then(function (result) {
                vm.hostelRooms = result;
            });
        }
    };

})(angular);