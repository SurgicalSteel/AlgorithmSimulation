package edit_distance

import (
	"errors"
	"fmt"
	"github.com/stretchr/testify/assert"
	"testing"
)

func TestCalculateEditDistance(t *testing.T) {
	type testcase struct {
		wordA            string
		wordB            string
		expectedDistance int
		expectedError    error
	}
	testcases := make(map[string]testcase)
	testcases["raise-error"] = testcase{
		wordA:            "banana",
		wordB:            "",
		expectedDistance: -1,
		expectedError:    errors.New("One or both string are empty."),
	}
	testcases["one-insert-one-delete"] = testcase{
		wordA:            "banana",
		wordB:            "ananas",
		expectedDistance: 2,
		expectedError:    nil,
	}
	testcases["no-alphabet-match"] = testcase{
		wordA:            "pavel",
		wordB:            "dmitry",
		expectedDistance: 6,
		expectedError:    nil,
	}
	testcases["exactly-match"] = testcase{
		wordA:            "bangun",
		wordB:            "bangun",
		expectedDistance: 0,
		expectedError:    nil,
	}
	testcases["insertion-distance"] = testcase{
		wordA:            "linda",
		wordB:            "melinda",
		expectedDistance: 2,
		expectedError:    nil,
	}
	for k, v := range testcases {
		fmt.Println("Running test on testcase :", k)
		actualDistance, actualError := CalculateEditDistance(v.wordA, v.wordB)
		if v.expectedError != nil {
			assert.Equal(t, v.expectedError, actualError)
		} else {
			assert.Equal(t, v.expectedDistance, actualDistance)
		}

	}
}
