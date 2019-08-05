package calculator

import (
	"errors"
	"math"
)

//CalculateDistance calculates distance of two given points using Haversine formula
func CalculateDistance(pointA, pointB *Coordinate) (float64, error) {
	var distance float64 = 0.0
	if pointA == nil || pointB == nil {
		return distance, errors.New("one or both points given is nil. Points must be clearly defined!")
	}

	var radPerDeg float64 = math.Pi / 180.0
	const earthRadiusKilometer float64 = 6371.0

	var deltaLatitudeRad float64 = (pointB.Latitude - pointA.Latitude) * radPerDeg
	var deltaLongitudeRad float64 = (pointB.Longitude - pointA.Longitude) * radPerDeg

	var latPointARad float64 = pointA.Latitude * radPerDeg
	var latPointBRad float64 = pointB.Latitude * radPerDeg

	var sinDeltaLatitudeRad float64 = math.Sin(deltaLatitudeRad / 2)
	var sinDeltaLongitudeRad float64 = math.Sin(deltaLongitudeRad / 2)

	var a float64 = squareFloat64(sinDeltaLatitudeRad) + (math.Cos(latPointARad) * math.Cos(latPointBRad) * squareFloat64(sinDeltaLongitudeRad))
	var c float64 = 2.0 * math.Atan2(math.Sqrt(a), math.Sqrt(1-a))
	distance = earthRadiusKilometer * c

	return distance, nil
}

func squareFloat64(a float64) float64 {
	return a * a
}
