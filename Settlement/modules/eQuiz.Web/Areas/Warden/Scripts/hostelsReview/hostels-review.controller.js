(function (angular) {
    angular.module('settlementModule').controller('HostelsReviewController', HostelsReviewController);

    ReviewController.$inject = ['$scope', '$filter', 'hostelsReviewDataService', 'hostelsList'];

    function ReviewController($scope, $filter, hostelsReviewDataService, hostelsList) {
        var vm = this;
        var orderBy = $filter('orderBy');
        vm.search = ''; // Represents search field on the form
        vm.myPredicate = null;
        vm.tablePage = 0; // Current table page
        vm.resultsPerPage = 10;
        vm.resultsCount = [10, 25, 50, 100]; // Possible numbers of results per page
        vm.selectedGroup = [];
        vm.hostelId;
        vm.newHostel = {};
        vm.newHostelBoxOpened = false;
        vm.changeHostelBoxOpened = false;
        vm.hostels = hostelsList;
        vm.changedHostel = {};

        vm.headers = [
    {
        name: 'Number',
        field: 'Number',
        predicateIndex: 0
    }, {
        name: 'Address',
        field: 'Address',
        predicateIndex: 1
    }, {
        name: 'Month Payment',
        field: 'MonthPaymentSum',
        predicateIndex: 2
    }
        ];

        vm.hostels = [];

        function activate() {
            hostelReviewsDataService.getHostels.then(function (respond) {
                vm.hostels = respond.data;
            })
                generatePredicate();
        };
        generatePredicate();

        function generatePredicate() {
            vm.myPredicate = [null, null, null];
        };

        function clearPredicatesExcept(index) {
            var temp = vm.myPredicate[index];
            generatePredicate();
            vm.myPredicate[index] = temp;
        }; // Clears all predicates except the one with a specified index

        vm.refreshPredicate = function (index) {
            if (vm.myPredicate[index] === null) {
                var item = null;
                switch (index) {
                    case 0:
                        item = '+Number';
                        break;
                    case 1:
                        item = '+Address';
                        break;
                    case 2:
                        item = '+MonthPaymentSum';
                        break;
                }
                vm.myPredicate[index] = item;
            }
            else if (vm.myPredicate[index][0] === '+') {
                vm.myPredicate[index] = '-' + vm.myPredicate[index].slice(1);
            }
            else if (vm.myPredicate[index][0] === '-') {
                vm.myPredicate[index] = null;
            }
            clearPredicatesExcept(index);
        }; // Changes the value of the predicate with specified index and clears all others

        vm.direction = function (index) {
            if (vm.myPredicate) {
                if (vm.myPredicate[index] === null) {
                    return null;
                };
                if (vm.myPredicate[index][0] === '+') {
                    return true;
                };
                return false;
            };
            return null;
        }; // Gets the order direction of the predicate with specified index

        vm.order = function (predicate, reverse) {
            vm.hostels = orderBy(vm.hostels, predicate, reverse);
            vm.predicate = predicate;
        }; // Orders the data based on the specified predicate

        vm.numberOfPages = function () {
            return Math.ceil(vm.searchFiltered.length / vm.resultsPerPage);
        };

        vm.getNumber = function (num) {
            return new Array(num);
        };

        vm.goToPage = function (page) {
            vm.tablePage = page;
        };

        vm.toggleChangeHostel = function (id) {
            console.log(id);
            vm.changeHostelBoxOpened = true;
            vm.temp = vm.hostels.filter(function (h) {
                return h.Id == id;
            })[0];
            angular.copy(vm.temp, vm.changedHostel);
        };

        vm.toggleAddHostel = function (id) {
            vm.addHostelBoxOpened = !vm.addHostelBoxOpened;
        };

        vm.saveHostel = function () {
            //vm.changedHostel.Id = vm.chosenRole.RoleId;
            hostelsReviewDataService.changeHostel(vm.changedHostel).success(function (res) {
                activate();
                vm.changeHostelBoxOpened = false;
                $scope.showNotifyPopUp('Hostel was successfully saved!')
                $timeout($scope.closePopUp, 4000);
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: hostel was not saved!')
                $timeout($scope.closePopUp, 4000);
                vm.changeHostelBoxOpened = false;
            });;
        }

        vm.addHostel = function () {
            //vm.newHostel.RoleId = vm.chosenRole.RoleId;
            hostelsReviewDataService.addHostel(vm.newHostel).success(function (res) {
                activate();
                vm.newHostelBoxOpened = false;
                $scope.showNotifyPopUp('New hostel was successfully added!')
                $timeout($scope.closePopUp, 4000);
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: new hostel was not added!')
                $timeout($scope.closePopUp, 4000);
                vm.newAccountBoxOpened = false;
            });;
        }

        // validate forms fuckin later
    };
})(angular);
