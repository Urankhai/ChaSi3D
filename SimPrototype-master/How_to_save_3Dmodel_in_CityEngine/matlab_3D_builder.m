clear all
close all

% file_name = 'Few_buildings2_0.obj';
% file_name2 = 'Few_buildings2_0.obj';

file_name2 = 'Shapes3/separate_Shape 9659.obj';

% fid = fopen(file_name);
% allText = textscan(fid,'%s','delimiter','\n');
% numberOfLines = length(allText{1});
% fclose(fid);

% tic
fid = fopen(file_name2);

vertices = [];
num_ver = 0;

faces = {};
num_fac = 0;

while ~feof(fid) % read until the end of the file
    tline = fgetl(fid);
    if ~ischar(tline), break, end
    
    if ~strcmp(tline,'')
%         keyboard
        if strcmp(tline(1:2),'v ')
            num_ver = num_ver + 1;
            asd = str2num(tline(3:end));
%             vertices(num_ver,:) = asd;
%             vertices{num_ver} = asd;
            vertices = [vertices; asd];
        end
        
        if strcmp(tline(1:2),'f ')
%             keyboard
            num_fac = num_fac + 1;
            zxc = str2num(tline(3:end));
            faces{num_fac} = zxc;
        end
    end
    
end

disp(['number of vertices = ', num2str(num_ver)])
disp(['number of faces = ', num2str(num_fac)])
fclose(fid);

% toc

% keyboard
figure(1)
axis equal
grid on
hold on
% plot3(vertices(1,1), vertices(1,2), vertices(1,3),'r*')
% plot3(vertices(:,1), vertices(:,2), vertices(:,3),'-o')
% plot3(vertices(end,1), vertices(end,2), vertices(end,3),'gd')


for i = 1:num_fac
%     plot3(vertices(faces{i},1), vertices(faces{i},2), vertices(faces{i},3),'r','linewidth',3)
    patch(vertices(faces{i},1), vertices(faces{i},2), vertices(faces{i},3),'c')
%     keyboard
end
% patch('Vertices',Tw2,'Faces',Fs2,'FaceVertexCData',[.4 .8 .8],'FaceColor','flat')
