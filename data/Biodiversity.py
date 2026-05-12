import math

# CalculateDistance calculates the distance in kilometres (km) between two locations. 
# CalculateDistance takes four variables, the latitude and longitude of the first 
# 	location and the latitude and longitude of the second location. It returns the
#	distance in km between the two locations. 

#Example
#Distance between Exeter (50.7, -3.533333) and Newcastle (54.988056, -1.619444)
#CalculateDistance (50.7, -3.533333, 54.988056, -1.619444) = 493.92 km

#Distance between London (51.508129, -0.128005) and New York (40.4521, 73.5911)
#CalculateDistance(51.508129, -0.128005, 40.4521, 73.5911) = 5579.49 km

def CalculateDistance(Lat1, Lon1, Lat2, Lon2):

    Lat1 = float(Lat1)
    Lon1 = float(Lon1)
    Lat2 = float(Lat2)
    Lon2 = float(Lon2)
    
    nDLat = (Lat1 - Lat2) * 0.017453293
    nDLon = (Lon1 - Lon2) * 0.017453293

    Lat1 = Lat1 * 0.017453293
    Lat2 = Lat2 * 0.017453293

    nA = (math.sin(nDLat/2) ** 2) + math.cos(Lat1) * math.cos(Lat2) * (math.sin(nDLon/2) ** 2 )
    nC = 2 * math.atan2(math.sqrt(nA),math.sqrt( 1 - nA ))
    nD = 6372.797 * nC

    return nD


# LineToList takes a string and converts it to a list breaking it on the tabs.
# new lines are remove
def LineToList(Str):
    Str = Str.rstrip()
    return Str.split("\t")