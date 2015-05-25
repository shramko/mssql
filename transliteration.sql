
-- exec string_transliteration '������ ���������'
alter procedure string_transliteration
(
	@inputstring varchar(8000),
	@transid int = null
) as
begin
	/*
		@transid - �� �������� ��������������. ������ ������������ ���������
		1 - � ������������ � ����������� 6 ������� ��� ������ �� 26 ��� 1997 �. N 310 http://rosadvokat.ru/open.php?id=11800742_1
		2 - � ������������ � ���� � 52535.1-2006 http://www.complexdoc.ru/pdf/%D0%93%D0%9E%D0%A1%D0%A2%20%D0%A0%2052535.1-2006/gost_r_52535.1-2006.pdf
		3 - � ������������ � �. 97 ������� ��� ������ N 320 �� 15 ������� 2012 �.[10] � ������������ � ��������������� ���� ������������� ���������� (Doc 9303, ����� 1, ���������� 9 � ������� IV) http://www.icao.int/publications/Documents/9303_p1_v1_cons_ru.pdf
	*/


	declare  
		 @outputstring varchar(8000)
		,@counter int		
		,@ch1 varchar(10)
		,@ch2 varchar(10)
		,@ch3 varchar(10)
	
	declare 
		@result_table table (id int, translate varchar(8000))

	------------------------------------------------------------------	
	------- 1 - � ������������ � ����������� 6 ������� ��� ������ �� 26 ��� 1997 �. N 310 ------------------	 	 
	 select  
		 @counter = 1
		,@outputstring = ''
	
	--���������� �������: � - ����� ����� �������� ���������� - ss - Goussev.
	declare @t1 table (ch char)
	insert into @t1
			  select '�'
		union select '�'
		union select '�'
		union select '�'
		union select '�'
		union select '�'
		union select '�'
		union select '�'
		union select '�'
		union select '�'
	
	declare @t2 table (ch char)
	insert into @t2
		select '�'
	
	declare @str varchar(4000) = ''	
	select @str = @str + t1.ch + t2.ch + t3.ch + '|' from @t1 t1, @t2 t2, @t1 t3
	-----------------------------------------------------------------------------
	
	while (@counter <= len(@inputstring))
	begin
		select @ch1 = substring(@inputstring,@counter,1)
		select @ch2 = substring(@inputstring,@counter,2)


		select  
			@outputstring = @outputstring + 
				case
					when J8 > 0 then 
									case 
										when @ch2 collate Cyrillic_General_CS_AS = upper(@ch2) then'INE'
										else 'ine'
									end
					when J7 > 0 then 
									case 
										when @ch2 collate Cyrillic_General_CS_AS = upper(@ch2) then'IE'
										else 'ie'
									end
					when J6 > 0 then 
									case 
										when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then'X'
										else 'x'
									end
					when J5 > 0 then 
									case 
										when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then 'SS'
										else 'ss'
									end
					when J4 > 0 then 
									case
										when @ch2 collate Cyrillic_General_CS_AS = upper(@ch2) then 'GUIA'	
										when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then 'Guia'																																																											
										else 'guia'
									end
					when J3 > 0 then 
									case
										when @ch2 collate Cyrillic_General_CS_AS = upper(@ch2) then 'GUIOU'	
										when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then 'Guiou'																																																											
										else 'guiou'
									end
					when J2 > 0 then replace(substring(
														case
															when @ch2 collate Cyrillic_General_CS_AS = upper(@ch2) then '|GUE|GUE|GUI|GUI|GUY'	
															when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then '|Gue|Gue|Gui|Gui|Guy'																																																											
															else '|gue|gue|gui|gui|guy'
														end, J2 + 1, 3), '|', '')
					when J1 > 0 then substring(
												case 
													when @ch2 collate Cyrillic_General_CS_AS = upper(@ch2) then 'OUKHTS�HIA'
													when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then 'OuKhTs�hIa'
													else 'oukhts�hia'
												end, J1*2 - 1, 2)
					when J11 > 0 then substring(
												case 
													when @ch2 collate Cyrillic_General_CS_AS = upper(@ch2) then 'TCHIOU'
													when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then 'TchIou'
													else 'tchiou'
												end, J11*J11, 3) 
					when J0 > 0 then substring(
												case 
													when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then 'ABVGDEEJZIYKLMNOPRSTFYE'
													else 'abvgdeejziyklmnoprstfye'
												end, J0, 1)
					else case 
							when @ch2 collate Cyrillic_General_CS_AS = upper(@ch2) then replace(@ch1,'�','SHTCH')
							when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then replace(@ch1,'�','Shtch')
							else replace(@ch1,'�','shtch')
						 end 
				end
			,@counter = @counter + 
				case					
					when J2 + J3 + J4 + J6 + J7 + J8 >0 then 2
					else 1
				end
		from (
			select  				
				 patindex('%|' + substring(@inputstring,@counter,3) + '|%','|�� |��,|��.|��;|��:|' )			as J8 -- ������� �� "��" ������� � "e" - Vassine - �����.
				,patindex('%|' + substring(@inputstring,@counter,2) + '|%','|��|ܨ|' )							as J7 -- ���� � ������� ����� "�" ������� "e", �� ������� "ie"
				,patindex('%|' + substring(@inputstring,@counter,2) + '|%','|��|' )								as J6 -- ��������� "��" �� ����������� ������ ������� ��� "�"
				,patindex('%|' + substring(@inputstring,@counter-1,3) + '|%','|'+ @str )						as J5 -- � - ����� ����� �������� ���������� - ss
				,patindex('%|' + substring(@inputstring,@counter,2) + '|%','|��|' )								as J4 --G,g ����� e, i, � ������� � "u" (gue, gui, guy)
				,patindex('%|' + substring(@inputstring,@counter,2) + '|%','|��|' )								as J3 --G,g ����� e, i, � ������� � "u" (gue, gui, guy)
				,patindex('%|' + substring(@inputstring,@counter,2) + '|%','|��||��||��||��||��|')				as J2 --G,g ����� e, i, � ������� � "u" (gue, gui, guy)
				,patindex('%'  + substring(@inputstring,@counter,1) +  '%','�����')								as J1
				,patindex('%' + substring(@inputstring,@counter,1)  +  '%','��')								as J11
				,patindex('%'  + substring(@inputstring,@counter,1) +  '%','�����Ũ������������������')			as J0
			) J
	end
	insert into @result_table
	select 1, @outputstring


	------------------------------------------------------------------	
	------- 2 - � ������������ � ���� � 52535.1-2006 ------------------	 
	 select  		 
		 @counter = 1
		,@outputstring = ''
		 
	while (@counter <= len(@inputstring))
	begin 	
		select @ch1 = substring(@inputstring,@counter,1)
		select @ch2 = substring(@inputstring,@counter,2)
		select  
			@outputstring = @outputstring + 
				case			
					
					when J1 > 0 then substring( case 
													when @ch2 collate Cyrillic_General_CS_AS = upper(@ch2) then 'ZHKHT�CHSHIAIU'
													when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then 'ZhKhTcChShIaIu'
													else 'zhkht�chshiaiu'
												end	, J1*2 - 1, 2)				

					when J0 > 0 then substring( case
													when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then 'ABVGDEEZIIKLMNOPRSTUFYE'
													else 'abvgdeeziiklmnoprstufye'
												end, J0, 1)				

					else case
							when @ch2 collate Cyrillic_General_CS_AS = upper(@ch2) then replace(@ch1,'�','SHCH')
							when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then replace(@ch1,'�','Shch')
							else replace(@ch1,'�','shch')
						 end
				end
			,@counter = @counter + 1		
		from (
				select  		 		 
					 patindex('%' + @ch1 + '%','�������') as J1
					,patindex('%' + @ch1 + '%','�����Ũ������������������'       ) as J0
			  ) J
	end

	insert into @result_table (id, translate)
		select 2, @outputstring
	

	------------------------------------------------------------------	
	------- 3 - � ������������ � �. 97 ������� ��� ������ N 320 �� 15 ������� 2012 �.[10] � ������������ � ��������������� ���� ������������� ���������� (Doc 9303, ����� 1, ���������� 9 � ������� IV) ------------------	 
	 select  		 
		 @counter = 1
		,@outputstring = ''
		 
	while (@counter <= len(@inputstring))
	begin 	
		select @ch1 = substring(@inputstring,@counter,1)
		select @ch2 = substring(@inputstring,@counter,2)

		select  
			@outputstring = @outputstring + 
				case			
					
					when J1 > 0 then substring( case 
													when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then 'ZHKHTSCHSHIAIUIE'
													else 'zhkhtschshiaiuie'
												end	, J1*2 - 1, 2)				

					when J0 > 0 then substring( case
													when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then 'ABVGDEEZIYKLMNOPRSTUFYE'
													else 'abvgdeeziyklmnoprstufye'
												end, J0, 1)				

					else case 
							when @ch2 collate Cyrillic_General_CS_AS = upper(@ch2) then replace(@ch1,'�','SHCH')
							when @ch1 collate Cyrillic_General_CS_AS = upper(@ch1) then replace(@ch1,'�','Shch')
							else replace(@ch1,'�','shch')
						 end
				end
			,@counter = @counter + 1		
		from (
				select  		 		 
					 patindex('%' + @ch1 + '%','��������') as J1
					,patindex('%' + @ch1 + '%','�����Ũ�����������������'       ) as J0
			  ) J
	end

	insert into @result_table (id, translate)
		select 3, @outputstring

	--------������� ���������------------------------	
	select * 
	from 
		@result_table
	where
		(@transid is not null and id = @transid)
		or (id is not null and @transid is null)

end
go



