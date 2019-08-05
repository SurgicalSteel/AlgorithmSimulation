package calculator

import (
	"errors"
	"fmt"
	"github.com/stretchr/testify/assert"
	"math"
	"testing"
)

const allowedRadiusError float64 = 1

func TestCalculateDistance(t *testing.T) {
	type testcase struct {
		pointA           *Coordinate
		pointB           *Coordinate
		expectedDistance float64
		expectedError    error
	}

	// define points using well known Airports
	// Soekarno-Hatta International Airport
	cgk := Coordinate{
		Latitude:  -6.07,
		Longitude: 106.39,
	}

	// Los Angeles International Airport
	lax := Coordinate{
		Latitude:  33.56,
		Longitude: -118.24,
	}

	// Moscow Domodedovo Airport
	dme := Coordinate{
		Latitude:  55.24,
		Longitude: 37.54,
	}

	// Saint Petersburg Pulkovo Airport
	led := Coordinate{
		Latitude:  59.48,
		Longitude: 30.15,
	}

	testCases := make(map[string]testcase)

	testCases["one nil error"] = testcase{
		pointA:           &led,
		pointB:           nil,
		expectedDistance: 0,
		expectedError:    errors.New("one or both points given is nil. Points must be clearly defined!"),
	}

	testCases["both null error"] = testcase{
		pointA:           nil,
		pointB:           nil,
		expectedDistance: 0,
		expectedError:    errors.New("one or both points given is nil. Points must be clearly defined!"),
	}

	testCases["LAX to DME"] = testcase{
		pointA:           &lax, // 33.56 N & 118.24 w
		pointB:           &dme, // 55.24 N & 37.54 E
		expectedDistance: 9875, //according to : https://www.movable-type.co.uk/scripts/latlong.html
		expectedError:    nil,
	}

	testCases["CGK to LED"] = testcase{
		pointA:           &cgk, // 6.07 S & 106.39 E
		pointB:           &led, // 59.48 N & 30.15 E
		expectedDistance: 9823, //according to : https://www.movable-type.co.uk/scripts/latlong.html
		expectedError:    nil,
	}

	for k, v := range testCases {
		fmt.Println("Running on testcase :", k)
		actualDistance, actualError := CalculateDistance(v.pointA, v.pointB)
		if actualError != nil {
			assert.Equal(t, v.expectedError, actualError)
		} else {
			delta, validDelta := checkDelta(v.expectedDistance, actualDistance, allowedRadiusError)
			if !validDelta {
				t.Errorf("radius error : %f exceeds allowed radius error (%f kilometers)", delta, allowedRadiusError)
			}
		}
	}

}

func checkDelta(expectedDistance, actualDistance, allowedRadiusError float64) (float64, bool) {
	delta := math.Abs(expectedDistance - actualDistance)
	fmt.Println("expected Distance :", expectedDistance)
	fmt.Println("actual Distance :", actualDistance)
	return delta, delta <= allowedRadiusError
}
