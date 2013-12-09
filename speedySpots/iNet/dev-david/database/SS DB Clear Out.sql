/* READ ME: This script will cearl out all production data from the database including companies and 
users EXCEPT for the user account associated with the mac@inetsolution.com account and it's associated org
*/

-- Delete Spots
DELETE FROM IASpotFile;

DELETE FROM IASpotFee;

DELETE FROM IASpot;

-- Delete PO
DELETE FROM IAProductionOrder;


-- DELETE Jobs
DELETE FROM IAJobFile;

DELETE FROM IARequestProduction;

DELETE FROM IAJob;


-- DELETE Requests
DELETE FROM IARequestProductionFile;

DELETE FROM IARequestNote;

DELETE FROM IARequestFile;

DELETE FROM IARequestEstimate;

DELETE FROM IARequest;


-- DELETE Orders
DELETE FROM IAOrderLineItem;

DELETE FROM IAOrder;

DELETE FROM IACustomerCreditCard;


-- DELETE Talent Info
DELETE FROM IATalentSchedule;

DELETE FROM IATalentUnavailability;



-- DELETE Org Data
DELETE FROM MPOrgData WHERE MPOrgID <> '943B972E-C136-4308-B711-D7A567120177';

DELETE FROM MPOrgUser WHERE MPUserID <> 'BF1B40D4-758A-48D8-BA4E-6F6400A104B1';

DELETE FROM MPOrg WHERE MPOrgID <> '943B972E-C136-4308-B711-D7A567120177';


-- DELETE Users
DELETE FROM MPUserData WHERE MPUserID <> 'BF1B40D4-758A-48D8-BA4E-6F6400A104B1';

DELETE FROM MPUserPasswordHistory WHERE MPUserID <> 'BF1B40D4-758A-48D8-BA4E-6F6400A104B1';

DELETE FROM MPUser WHERE MPUserID <> 'BF1B40D4-758A-48D8-BA4E-6F6400A104B1';
