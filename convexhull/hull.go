package main

import (
	"fmt"
	"sort"
)

// LeftMost defines sort by leftmost x
type LeftMost []Point

func (lm LeftMost) Len() int {
	return len(lm)
}
func (lm LeftMost) Swap(i, j int) {
	lm[i], lm[j] = lm[j], lm[i]
}
func (lm LeftMost) Less(i, j int) bool {
	return (lm[i].X < lm[j].X)
}
func checkTurn(p, q, r Point) int {
	res := (r.X-q.X)*(p.Y-q.Y) - (r.Y-q.Y)*(p.X-q.X)
	if res < 0 {
		return -1 //right turn
	} else if res > 0 {
		return 1 //left turn
	} else {
		return 0 //straight line
	}
}

func checkLastThreePoint(pointSet []Point) bool {
	if len(pointSet) < 3 {
		return false
	}
	size := len(pointSet)
	checkerValue := checkTurn(pointSet[size-3], pointSet[size-2], pointSet[size-1])
	return (checkerValue >= 0)
}

func matchTest(result []Point) bool {
	checkerPoints := GenerateCheckData()
	if len(result) != len(checkerPoints) {
		fmt.Printf("INCORRECT CONVEX HULL SIZE, Expecting %d , but got %d\n", len(checkerPoints), len(result))
		return false
	}
	sort.Sort(LeftMost(result))
	sort.Sort(LeftMost(checkerPoints))

	for i := 0; i < len(result); i++ {
		for j := 0; j < len(checkerPoints); j++ {
			if !(result[i].X == checkerPoints[i].X && result[i].Y == checkerPoints[i].Y) {
				return false
			}
		}
	}
	return true
}

// ConvexHull find subset of points belong to the convex hull from given points
func ConvexHull(pointSet []Point) []Point {
	if len(pointSet) < 2 {
		fmt.Printf("REQUIRES AT LEAST 2 POINTS!\n")
		return pointSet
	}
	sort.Sort(LeftMost(pointSet))
	upperL := []Point{}
	upperL = append(upperL, pointSet[0])
	upperL = append(upperL, pointSet[1])
	for i := 2; i < len(pointSet); i++ {
		upperL = append(upperL, pointSet[i])
		for len(upperL) > 2 && checkLastThreePoint(upperL) {
			sizeL := len(upperL)
			upperL = append(upperL[:(sizeL-2)], upperL[(sizeL-1):]...)
		}
	}
	lowerL := []Point{}
	lowerL = append(lowerL, pointSet[len(pointSet)-1])
	lowerL = append(lowerL, pointSet[len(pointSet)-2])
	for i := len(pointSet) - 3; i >= 0; i-- {
		lowerL = append(lowerL, pointSet[i])
		for len(lowerL) > 2 && checkLastThreePoint(lowerL) {
			sizeL := len(lowerL)
			lowerL = append(lowerL[:(sizeL-2)], lowerL[(sizeL-1):]...)
		}
	}
	tempLowerL := []Point{}
	for i := 2; i < len(lowerL)-1; i++ {
		tempLowerL = append(lowerL, lowerL[i])
	}

	upperL = append(upperL, tempLowerL...)

	return removeDuplicate(upperL)
}

func removeDuplicate(pointSet []Point) []Point {
	mapper := make(map[float64]map[float64]bool)
	for i := 0; i < len(pointSet); i++ {
		if _, ok := mapper[pointSet[i].X]; !ok {
			val := make(map[float64]bool)
			val[pointSet[i].Y] = true
			mapper[pointSet[i].X] = val
		} else {
			if _, ok := mapper[pointSet[i].X][pointSet[i].Y]; !ok {
				mapper[pointSet[i].X][pointSet[i].Y] = true
			}
		}
	}
	result := []Point{}
	for km, vm := range mapper {
		for kkm := range vm {
			result = append(result, Point{X: km, Y: kkm})
		}
	}
	return result
}

func main() {
	result := ConvexHull(GenerateTestData())
	sort.Sort(LeftMost(result))
	for i := 0; i < len(result); i++ {
		fmt.Println(result[i])
	}
	fmt.Println("RESULT LENGTH : ", len(result))
	if matchTest(result) {
		fmt.Println("Result matches with test data")
	} else {
		fmt.Println("Result does not match with test data")
	}

}
