package LongestCommonSubsequence

import (
	"errors"
	"fmt"
	"github.com/stretchr/testify/assert"
	"testing"
)

func TestFindLCS(t *testing.T) {
	type testcase struct {
		wordA               string
		wordB               string
		expectedSubsequence []string
		expectedError       error
	}
	testcases := make(map[string]testcase)
	testcases["error empty sa"] = testcase{
		wordA:               "",
		wordB:               "BATUSHKA",
		expectedSubsequence: make([]string, 0),
		expectedError:       errors.New("One or both given strings are empty"),
	}
	testcases["error empty sb"] = testcase{
		wordA:               "BATUSHKA",
		wordB:               "",
		expectedSubsequence: make([]string, 0),
		expectedError:       errors.New("One or both given strings are empty"),
	}
	testcases["error empty both"] = testcase{
		wordA:               "",
		wordB:               "",
		expectedSubsequence: make([]string, 0),
		expectedError:       errors.New("One or both given strings are empty"),
	}

	testcases["a short DNA string"] = testcase{ //taken from https://www.commonlounge.com/discussion/d73146874907470ba34c54a22214d067
		wordA:               "TAGTCACG",
		wordB:               "AGACTGTC",
		expectedSubsequence: []string{"AGACG"},
		expectedError:       nil,
	}

	for k, v := range testcases {
		fmt.Println("test case:", k)
		actualSubsequence, actualError := FindAllLCS(v.wordA, v.wordB)
		assert.Equal(t, v.expectedSubsequence, actualSubsequence)
		assert.Equal(t, v.expectedError, actualError)
	}
}
