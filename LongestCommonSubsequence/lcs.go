//Package LongestCommonSubsequence provides utility function to find the Longest Common Subsequence (LCS) from two given strings.
package LongestCommonSubsequence

import (
	"errors"
	"fmt"
)

type cell struct {
	i int
	j int
}

//FindAllLCS is used to find the LCS from two given strings.
func FindAllLCS(sa, sb string) ([]string, error) {
	longestCommonSubsequences := make([]string, 0)
	if len(sa) == 0 || len(sb) == 0 {
		return longestCommonSubsequences, errors.New("One or both given strings are empty")
	}
	mx := len(sa) + 1
	my := len(sb) + 1
	m := make([][]int, mx)
	for i := 0; i < mx; i++ {
		m[i] = make([]int, my)
	}
	for j := 0; j < my; j++ {
		for i := 0; i < mx; i++ {
			if i == 0 || j == 0 {
				m[i][j] = 0
			}
		}
	}

	//to store the longest substring length
	commonSubstringLength := 0
	for j := 1; j < my; j++ {
		for i := 1; i < mx; i++ {
			if sa[i-1] == sb[j-1] {
				m[i][j] = m[i-1][j-1] + 1
				commonSubstringLength = max(commonSubstringLength, m[i][j])
			} else {
				m[i][j] = max(m[i-1][j], m[i][j-1])
			}
		}
	}

	//find all the LCS end, store them in a slice. Each of them will be backtracked.
	lcsEnd := make([]cell, 0)
	for j := 1; j < my; j++ {
		for i := 1; i < mx; i++ {
			if m[i][j] == commonSubstringLength {
				lcsEnd = append(lcsEnd, cell{i: i, j: j})
			}
		}
	}
	// for ile := 0; ile < len(lcsEnd); ile++ {
	// 	fmt.Println(lcsEnd[ile].i, lcsEnd[ile].j)
	// }
	// fmt.Println()

	//do backtrack for each LCS End to find the common subsequence
	for ile := 0; ile < len(lcsEnd); ile++ {
		temporaryResult := ""
		bx, by := lcsEnd[ile].i, lcsEnd[ile].j //len(sa), len(sb)
		reversedSubstring := ""
		for m[bx][by] > 0 /*bx > 0 && by > 0*/ {
			if m[bx][by-1] > m[bx-1][by] {
				by--
			} else if m[bx-1][by] > m[bx][by-1] {
				bx--
			} else {
				if (m[bx-1][by-1] + 1) == m[bx][by] {
					reversedSubstring = reversedSubstring + sa[(bx-1):bx]
					bx--
					by--
				} else {
					bx--
				}

			}
		}

		temporaryResult = reverse(reversedSubstring)
		longestCommonSubsequences = append(longestCommonSubsequences, temporaryResult)
	}
	longestCommonSubsequences = removeDuplicateSubsequence(longestCommonSubsequences)
	// fmt.Println(longestCommonSubsequences)
	// printMatrix(m, my, mx)
	return longestCommonSubsequences, nil
}

func max(a, b int) int {
	if a > b {
		return a
	}
	return b
}

func reverse(s string) string {
	result := ""
	for i := len(s); i > 0; i-- {
		result = result + s[i-1:i]
	}
	return result
}

func printMatrix(m [][]int, r, c int) {
	for j := 0; j < r; j++ {
		for i := 0; i < c; i++ {
			fmt.Printf("%d ", m[i][j])
		}
		fmt.Println()
	}
}

func removeDuplicateSubsequence(subsequences []string) []string {
	uniqueSubsequences := make([]string, 0)
	subsequenceMap := make(map[string]int)
	for _, subsequence := range subsequences {
		if _, ok := subsequenceMap[subsequence]; !ok {
			subsequenceMap[subsequence] = 1
		}
	}
	for ks, _ := range subsequenceMap {
		uniqueSubsequences = append(uniqueSubsequences, ks)
	}
	return uniqueSubsequences
}
