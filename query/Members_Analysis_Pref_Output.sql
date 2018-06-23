--Active Members 895
select count(1) from MemPrefBooks mp where status=1
--Active Members with preference details 879
select count(1) from MemPrefBooks mp where status=1 and PrefBooks<>''
--Active Members with Tamil preference 435
select count(1) from MemPrefBooks mp where status=1 and PrefBooks like '%TM%'
--Active Members with Children preference 552
select count(1) from MemPrefBooks mp where status=1 and PrefBooks like '%ch%'
--Active Members with Romance preference 315
select count(1) from MemPrefBooks mp where status=1 and PrefBooks like '%rm%'
--Active Members with Thriller preference 450
select count(1) from MemPrefBooks mp where status=1 and PrefBooks like '%th%'
--Active Members with India preference 346
select count(1) from MemPrefBooks mp where status=1 and PrefBooks like '%in%'
--Active Members with Tamil and Children preference 256
select count(1) from MemPrefBooks mp where status=1 and PrefBooks like '%TM%'and PrefBooks like '%ch%'
--Active Members with Tamil and Children preference 209
select count(1) from MemPrefBooks mp where status=1 and PrefBooks like '%TM%'and PrefBooks like '%th%'
--Active Members with Tamil and Romance preference 160
select count(1) from MemPrefBooks mp where status=1 and PrefBooks like '%TM%'and PrefBooks like '%rm%'
--Active Members with Romance and Children preference 214
select count(1) from MemPrefBooks mp where status=1 and PrefBooks like '%ch%'and PrefBooks like '%rm%'
--Active Members with thriller and Children preference 289
select count(1) from MemPrefBooks mp where status=1 and PrefBooks like '%ch%'and PrefBooks like '%th%'