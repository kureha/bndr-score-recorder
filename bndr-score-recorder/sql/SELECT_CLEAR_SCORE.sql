SELECT 
	MS.title, MS.difficult, 
	MIN(TSR.good + TSR.bad + TSR.miss) missCount,
	CASE 
		WHEN MIN(TSR.good + TSR.bad + TSR.miss) = 0 THEN 'FULL COMBO'
		WHEN MIN(TSR.bad + TSR.miss) < 10 THEN 'HARD CLEAR'
		WHEN MIN(TSR.bad + TSR.miss) < 20 THEN 'NORMAL CLEAR'
		ELSE 'EASY CLEAR'
	END clearStatus
FROM 
T_SCORE_RECORD TSR 
INNER JOIN M_MUSIC MS 
ON TSR.music_id = MS.id
WHERE 
MS.level = #{id}
GROUP BY MS.id
ORDER BY missCount