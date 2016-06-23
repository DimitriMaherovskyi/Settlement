(function (angular) {
    angular.module('settlementModule').controller('QuotesReviewController', QuotesReviewController);

    QuotesReviewController.$inject = ['$scope', '$filter', 'quoteReviewDataService', 'quotesList'];

    function QuotesReviewController($scope, $filter, quoteReviewDataService, quotesList) {
        var vm = this;
        var orderBy = $filter('orderBy');
        vm.search = ''; // Represents search field on the form
        vm.myPredicate = null;
        vm.tablePage = 0; // Current table page
        vm.resultsPerPage = 10;
        vm.resultsCount = [10, 25, 50, 100]; // Possible numbers of results per page
        vm.selectedGroup = [];
        vm.newQuoteValue;
        vm.userId;
        vm.changeQuoteBoxOpened = false;
        vm.quotes = quotesList;

        vm.headers = [
    {
        name: 'Name',
        field: 'Name',
        predicateIndex: 0
    }, {
        name: 'Status',
        field: 'Status',
        predicateIndex: 1
    }, {
        name: 'Institute',
        field: 'Institute',
        predicateIndex: 2
    }, {
        name: 'Quote',
        field: 'Quote',
        predicateIndex: 3
    },
        ];

        function activate() {
                quoteReviewDataService.getQuotes();//.then(function (respond) {
                // vm.quotes = respond.data;
                //})
            generatePredicate();
        };
        generatePredicate();

        function generatePredicate() {
            vm.myPredicate = [null, null, null, null];
        }; // Generates empty predicates that are used for ordering

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
                        item = '+Name';
                        break;
                    case 1:
                        item = '+Status';
                        break;
                    case 2:
                        item = '+Institute';
                        break;
                    case 3:
                        item = '+Quote';
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
            vm.quotes = orderBy(vm.quotes, predicate, reverse);
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

        vm.toggleChangeQuote = function(id) {
            vm.changeQuoteBoxOpened = !vm.changeQuoteBoxOpened;
            vm.userId = id;
            var result = vm.quotes.filter(function(v) {
                return v.UserId === id;
            })[0].Quote;
            vm.newQuoteValue = result;
            
        };

        vm.saveQuote = function () {
            quoteReviewDataService.changeQuote(vm.userId, vm.newQuoteValue);
            activate();
        }
    };

})(angular);