package edit_distance

import (
	"errors"
)

// Calculate the edit distance of two given strings. Returns the distance and its error (mostly related to the parameter)
func CalculateEditDistance(sa, sb string) (int, error) {
	var count int = 0
	la := len(sa)
	lb := len(sb)
	if la == 0 || lb == 0 {
		return -1, errors.New("One or both string are empty.")
	}
	ela := la + 1
	elb := lb + 1
	m := make([][]int, ela)
	for i := 0; i < ela; i++ {
		m[i] = make([]int, elb)
	}
	for i := 0; i < ela; i++ {
		for j := 0; j < elb; j++ {
			m[i][j] = 0
		}
	}
	for i := 0; i < ela; i++ {
		for j := 0; j < elb; j++ {
			if j == 0 {
				m[i][j] = i
			} else if i == 0 {
				m[i][j] = j
			}
		}
	}
	for i := 0; i < la; i++ {
		for j := 0; j < lb; j++ {
			if sa[i] == sb[j] {
				m[i+1][j+1] = m[i][j]
			} else {
				mini := min(min(m[i][j], m[i+1][j]), m[i][j+1])
				m[i+1][j+1] = mini + 1
			}
		}
	}

	count = m[la][lb]
	return count, nil
}

func min(a, b int) int {
	if a < b {
		return a
	}
	return b
}
